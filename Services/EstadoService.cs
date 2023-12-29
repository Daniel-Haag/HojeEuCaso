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
    public class EstadoService : IEstadoService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public EstadoService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Estado> GetAllEstados()
        {
            return _HojeEuCasoDbContext.Estados
                .Include(x => x.Pais)
                .ToList();
        }

        public Estado GetEstadoById(int ID)
        {
            return _HojeEuCasoDbContext.Estados
                .Include(x => x.Pais)
                .Where(x => x.EstadoID == ID)
                .FirstOrDefault();
        }

        public void CreateNewEstado(Estado Estado)
        {
            try
            {
                _HojeEuCasoDbContext.Estados.Add(Estado);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateEstado(Estado Estado)
        {
            try
            {
                _HojeEuCasoDbContext.Estados.Update(Estado);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteEstado(int ID)
        {
            try
            {
                var Estado = GetEstadoById(ID);
                _HojeEuCasoDbContext.Estados.Remove(Estado);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
