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
    public class FotoServicoService : IFotoServicoService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public FotoServicoService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<FotosServicos> GetAllFotoServicos()
        {
            return _HojeEuCasoDbContext.FotosServicos
                .AsNoTracking()
                .ToList();
        }

        public FotosServicos GetFotoServicoById(int ID)
        {
            return _HojeEuCasoDbContext.FotosServicos
                .AsNoTracking()
                .Where(x => x.FotosServicosID == ID)
                .FirstOrDefault();
        }

        public List<FotosServicos> GetFotosServicoByServiceId(int ID)
        {
            return _HojeEuCasoDbContext.FotosServicos
                .AsNoTracking()
                .Where(x => x.PacoteID == ID)
                .ToList();
        }

        public void CreateNewFotoServico(FotosServicos FotoServico)
        {
            try
            {
                _HojeEuCasoDbContext.FotosServicos.Add(FotoServico);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateFotoServico(FotosServicos FotoServico)
        {
            try
            {
                _HojeEuCasoDbContext.FotosServicos.Update(FotoServico);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteFotoServico(int ID)
        {
            try
            {
                var FotoServico = GetFotoServicoById(ID);
                _HojeEuCasoDbContext.FotosServicos.Remove(FotoServico);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
