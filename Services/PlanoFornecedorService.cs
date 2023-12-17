using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public void Teste()
        {
            var ID = _fornecedorService.GetLoggedFornecedorID();
            var fornecedor = _fornecedorService.GetFornecedorById(ID);
            var planoFornecedor = GetAllPlanosFornecedores()
                .Where(x => x.FornecedorID == ID);

            //Agora com o planoFOrnecedor posso obter a chave da cobrança e solicitar o Status
        }
    }
}
