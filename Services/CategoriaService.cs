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
    public class CategoriaService : ICategoriaService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public CategoriaService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Categoria> GetAllCategorias()
        {
            return _HojeEuCasoDbContext.Categorias.Include(x => x.TipoCategoria).ToList();
        }

        public Categoria GetCategoriaById(int ID)
        {
            return _HojeEuCasoDbContext.Categorias.Where(x => x.CategoriaID == ID).FirstOrDefault();
        }

        public void CreateNewCategoria(Categoria Categoria)
        {
            try
            {
                _HojeEuCasoDbContext.Categorias.Add(Categoria);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateCategoria(Categoria Categoria)
        {
            try
            {
                _HojeEuCasoDbContext.Categorias.Update(Categoria);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteCategoria(int ID)
        {
            try
            {
                var Categoria = GetCategoriaById(ID);
                _HojeEuCasoDbContext.Categorias.Remove(Categoria);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
