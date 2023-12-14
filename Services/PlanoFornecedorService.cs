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

        public PlanoFornecedorService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
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
    }
}
