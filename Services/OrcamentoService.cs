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
    public class OrcamentoService : IOrcamentoService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public OrcamentoService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Orcamento> GetAllOrcamentos()
        {
            return _HojeEuCasoDbContext.Orcamentos
                .Include(x => x.WedManager)
                .ToList();
        }

        public Orcamento GetOrcamentoById(int ID)
        {
            return _HojeEuCasoDbContext.Orcamentos
                .Include(x => x.WedManager)
                .Where(x => x.OrcamentoID == ID)
                .FirstOrDefault();
        }

        public void CreateNewOrcamento(Orcamento Orcamento)
        {
            try
            {
                _HojeEuCasoDbContext.Orcamentos.Add(Orcamento);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateOrcamento(Orcamento Orcamento)
        {
            try
            {
                _HojeEuCasoDbContext.Orcamentos.Update(Orcamento);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteOrcamento(int ID)
        {
            try
            {
                var Orcamento = GetOrcamentoById(ID);
                _HojeEuCasoDbContext.Orcamentos.Remove(Orcamento);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
