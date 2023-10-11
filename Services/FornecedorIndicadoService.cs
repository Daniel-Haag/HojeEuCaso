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
    public class FornecedorIndicadoService : IFornecedorIndicadoService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public FornecedorIndicadoService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<FornecedorIndicado> GetAllFornecedorIndicados()
        {
            return _HojeEuCasoDbContext.FornecedoresIndicados
                .Include(x => x.FornecedorPai)
                .ToList();
        }

        public FornecedorIndicado GetFornecedorIndicadoById(int ID)
        {
            return _HojeEuCasoDbContext.FornecedoresIndicados
                .Where(x => x.FornecedorIndicadoID == ID)
                .FirstOrDefault();
        }

        public void CreateNewFornecedorIndicado(FornecedorIndicado FornecedorIndicado)
        {
            try
            {
                _HojeEuCasoDbContext.FornecedoresIndicados.Add(FornecedorIndicado);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateFornecedorIndicado(FornecedorIndicado FornecedorIndicado)
        {
            try
            {
                _HojeEuCasoDbContext.FornecedoresIndicados.Update(FornecedorIndicado);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteFornecedorIndicado(int ID)
        {
            try
            {
                var FornecedorIndicado = GetFornecedorIndicadoById(ID);
                _HojeEuCasoDbContext.FornecedoresIndicados.Remove(FornecedorIndicado);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
