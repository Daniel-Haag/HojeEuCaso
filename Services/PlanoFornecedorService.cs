using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using HojeEuCaso.Dtos;
using Newtonsoft.Json;
using System.Numerics;

namespace HojeEuCaso.Services
{
    public class PlanoFornecedorService : IPlanoFornecedorService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;
        private readonly IFornecedorService _fornecedorService;

        public PlanoFornecedorService(HojeEuCasoDbContext HojeEuCasoDbContext,
            IFornecedorService fornecedorService)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
            _fornecedorService = fornecedorService;
        }

        public List<PlanoFornecedor> GetAllPlanosFornecedores()
        {
            return _HojeEuCasoDbContext.PlanosFornecedores
                .Include(x => x.Fornecedor)
                .Include(x => x.Plano)
                .ToList();
        }

        public PlanoFornecedor GetPlanoFornecedorById(int ID)
        {
            return _HojeEuCasoDbContext.PlanosFornecedores
                .Include(x => x.Fornecedor)
                .Include(x => x.Plano)
                .Where(x => x.PlanoFornecedorID == ID)
                .FirstOrDefault();
        }

        public void CreateNewPlanoFornecedor(PlanoFornecedor PlanoFornecedor)
        {
            try
            {
                _HojeEuCasoDbContext.PlanosFornecedores.Add(PlanoFornecedor);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdatePlanoFornecedor(PlanoFornecedor PlanoFornecedor)
        {
            try
            {
                _HojeEuCasoDbContext.PlanosFornecedores.Update(PlanoFornecedor);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeletePlanoFornecedor(int ID)
        {
            try
            {
                var PlanoFornecedor = GetPlanoFornecedorById(ID);
                _HojeEuCasoDbContext.PlanosFornecedores.Remove(PlanoFornecedor);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }

        public async void AtualizaStatusPlanoFornecedorConformeAsaas()
        {
            try
            {
                var options = new RestClientOptions("https://sandbox.asaas.com/api/v3/payments");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("access_token", "$aact_YTU5YTE0M2M2N2I4MTliNzk0YTI5N2U5MzdjNWZmNDQ6OjAwMDAwMDAwMDAwMDAwNjYyNDY6OiRhYWNoX2ZkNjdjZWY0LTViMmYtNDU2NS1iMTk2LWYyZWEzOGIyMGRjNw==");
                var response = await client.GetAsync(request);

                if (response.IsSuccessful)
                {
                    string jsonResponse = response.Content;
                    var responseObject = JsonConvert.DeserializeObject<PagamentoListDto>(jsonResponse);
                    List<PagamentoAsaasResponseDto> pagamentos = responseObject?.Data ?? new List<PagamentoAsaasResponseDto>();

                    foreach (var pagamento in pagamentos)
                    {
                        if (pagamento.Status == "Paid")
                        {
                            var planoFornecedor = GetAllPlanosFornecedores().FirstOrDefault(x => x.AsaasPaymentID == pagamento.Id);

                            if (planoFornecedor != null)
                            {
                                planoFornecedor.Pago = true;
                                UpdatePlanoFornecedor(planoFornecedor);
                            }
                        }
                    }
                }

                Console.WriteLine("{0}", response.Content);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na rotina de atualização de status dos planos dos fornecedores!");
            }
            

        }
    }
}
