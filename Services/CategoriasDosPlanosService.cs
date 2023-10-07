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
    public class CategoriasDosPlanosService : ICategoriasDosPlanosService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public CategoriasDosPlanosService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<CategoriaDosPlanos> GetAllCategoriasDosPlanos()
        {
            return _HojeEuCasoDbContext.CategoriasDosPlanos
                .Include(x => x.Categoria)
                .Include(x => x.Plano)
                .ToList();
        }

        public CategoriaDosPlanos GetCategoriasDosPlanosById(int ID)
        {
            return _HojeEuCasoDbContext.CategoriasDosPlanos.Where(x => x.CategoriaDosPlanosID == ID).FirstOrDefault();
        }

        public void CreateNewCategoriasDosPlanos(CategoriaDosPlanos CategoriasDosPlanos)
        {
            try
            {
                _HojeEuCasoDbContext.CategoriasDosPlanos.Add(CategoriasDosPlanos);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateCategoriasDosPlanos(CategoriaDosPlanos CategoriasDosPlanos)
        {
            try
            {
                _HojeEuCasoDbContext.CategoriasDosPlanos.Update(CategoriasDosPlanos);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteCategoriasDosPlanos(int ID)
        {
            try
            {
                var CategoriasDosPlanos = GetCategoriasDosPlanosById(ID);
                _HojeEuCasoDbContext.CategoriasDosPlanos.Remove(CategoriasDosPlanos);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
