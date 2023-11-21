using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HojeEuCaso.Services
{
    public class PaisService : IPaisService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public PaisService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Pais> GetAllPaises()
        {
            return _HojeEuCasoDbContext.Paises.ToList();
        }

        public Pais GetPaisById(int ID)
        {
            return _HojeEuCasoDbContext.Paises
                .Where(x => x.PaisID == ID)
                .FirstOrDefault();
        }

        public void CreateNewPais(Pais Pais)
        {
            try
            {
                _HojeEuCasoDbContext.Paises.Add(Pais);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdatePais(Pais Pais)
        {
            try
            {
                _HojeEuCasoDbContext.Paises.Update(Pais);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeletePais(int ID)
        {
            try
            {
                var Pais = GetPaisById(ID);
                _HojeEuCasoDbContext.Paises.Remove(Pais);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
