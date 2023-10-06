using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HojeEuCaso.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public PlanoService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<Plano> GetAllPlanos()
        {
            return _HojeEuCasoDbContext.Planos.ToList();
        }

        public Plano GetPlanoById(int ID)
        {
            return _HojeEuCasoDbContext.Planos.Where(x => x.PlanoID == ID).FirstOrDefault();
        }

        public void CreateNewPlano(Plano Plano)
        {
            try
            {
                _HojeEuCasoDbContext.Planos.Add(Plano);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdatePlano(Plano Plano)
        {
            try
            {
                _HojeEuCasoDbContext.Planos.Update(Plano);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeletePlano(int ID)
        {
            try
            {
                var Plano = GetPlanoById(ID);
                _HojeEuCasoDbContext.Planos.Remove(Plano);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
