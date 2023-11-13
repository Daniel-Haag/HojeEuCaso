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
    public class ClausulaContratoService : IClausulaContratoService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public ClausulaContratoService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<ClausulaContrato> GetAllClausulaContratos()
        {
            return _HojeEuCasoDbContext.ClausulasContratos
                .Include(x => x.Categoria).ToList();
        }

        public List<ClausulaContrato> GetClausulasDeContratosByFornecedorID(int ID)
        {
            return _HojeEuCasoDbContext.ClausulasContratos
                .Include(x => x.Categoria)
                .Include(x => x.Fornecedor)
                .Where(x => x.Fornecedor.FornecedorID == ID)
                .ToList();
        }

        public ClausulaContrato GetClausulaContratoById(int ID)
        {
            return _HojeEuCasoDbContext.ClausulasContratos
                .Where(x => x.ClausulaContratoID == ID).FirstOrDefault();
        }

        public void CreateNewClausulaContrato(ClausulaContrato ClausulaContrato)
        {
            try
            {
                _HojeEuCasoDbContext.ClausulasContratos.Add(ClausulaContrato);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateClausulaContrato(ClausulaContrato ClausulaContrato)
        {
            try
            {
                _HojeEuCasoDbContext.ClausulasContratos.Update(ClausulaContrato);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteClausulaContrato(int ID)
        {
            try
            {
                var ClausulaContrato = GetClausulaContratoById(ID);
                _HojeEuCasoDbContext.ClausulasContratos.Remove(ClausulaContrato);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
