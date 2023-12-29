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
    public class CidadeService : ICidadeService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public CidadeService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Cidade> GetAllCidades()
        {
            return _HojeEuCasoDbContext.Cidades
                .Include(x => x.Estado)
                .ToList();
        }

        public Cidade GetCidadeById(int ID)
        {
            return _HojeEuCasoDbContext.Cidades
                .Include(x => x.Estado)
                .Where(x => x.CidadeID == ID)
                .FirstOrDefault();
        }

        public void CreateNewCidade(Cidade Cidade)
        {
            try
            {
                _HojeEuCasoDbContext.Cidades.Add(Cidade);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateCidade(Cidade cidade)
        {
            try
            {
                _HojeEuCasoDbContext.Cidades.Update(cidade);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteCidade(int ID)
        {
            try
            {
                var cidade = GetCidadeById(ID);
                _HojeEuCasoDbContext.Cidades.Remove(cidade);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
