using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HojeEuCaso.Services
{
    public class TipoCategoriaService : ITipoCategoriaService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public TipoCategoriaService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<TipoCategoria> GetAllTipoCategorias()
        {
            return _HojeEuCasoDbContext.TiposCategorias.ToList();
        }

        public TipoCategoria GetTipoCategoriaById(int ID)
        {
            return _HojeEuCasoDbContext.TiposCategorias.Where(x => x.TipoCategoriaID == ID).FirstOrDefault();
        }

        public void CreateNewTipoCategoria(TipoCategoria TipoCategoria)
        {
            try
            {
                _HojeEuCasoDbContext.TiposCategorias.Add(TipoCategoria);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateTipoCategoria(TipoCategoria TipoCategoria)
        {
            try
            {
                _HojeEuCasoDbContext.TiposCategorias.Update(TipoCategoria);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteTipoCategoria(int ID)
        {
            try
            {
                var TipoCategoria = GetTipoCategoriaById(ID);
                _HojeEuCasoDbContext.TiposCategorias.Remove(TipoCategoria);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
