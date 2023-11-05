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
    public class ItensDePacotesService : IItensDePacotesService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public ItensDePacotesService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<ItensDePacotes> GetAllItensDePacotes()
        {
            return _HojeEuCasoDbContext.ItensDePacotes
                .Include(x => x.Pacote)
                .AsNoTracking()
                .ToList();
        }

        public ItensDePacotes GetItensDePacotesById(int ID)
        {
            return _HojeEuCasoDbContext.ItensDePacotes
                .Where(x => x.ItensDePacotesID == ID)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public List<ItensDePacotes> GetItensDePacotesByPacoteId(int ID)
        {
            return _HojeEuCasoDbContext.ItensDePacotes
                .Include(x => x.Pacote)
                .Where(x => x.PacoteID == ID)
                .AsNoTracking()
                .ToList();
        }

        public void CreateNewItensDePacotes(ItensDePacotes ItensDePacotes)
        {
            try
            {
                _HojeEuCasoDbContext.ItensDePacotes.Add(ItensDePacotes);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateItensDePacotes(ItensDePacotes ItensDePacotes)
        {
            try
            {
                _HojeEuCasoDbContext.ItensDePacotes.Update(ItensDePacotes);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteItensDePacotes(int ID)
        {
            try
            {
                var ItensDePacotes = GetItensDePacotesById(ID);
                _HojeEuCasoDbContext.ItensDePacotes.Remove(ItensDePacotes);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
