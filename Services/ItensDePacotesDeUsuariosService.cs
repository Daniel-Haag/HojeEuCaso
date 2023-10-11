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
    public class ItensDePacotesDeUsuariosService : IItensDePacotesDeUsuariosService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public ItensDePacotesDeUsuariosService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<ItensDePacotesDeUsuarios> GetAllItensDePacotesDeUsuarios()
        {
            return _HojeEuCasoDbContext.ItensDePacotesDeUsuarios
                .Include(x => x.Pacote)
                .ToList();
        }

        public ItensDePacotesDeUsuarios GetItensDePacotesDeUsuariosById(int ID)
        {
            return _HojeEuCasoDbContext.ItensDePacotesDeUsuarios.Where(x => x.ItensDePacotesDeUsuariosID == ID).FirstOrDefault();
        }

        public void CreateNewItensDePacotesDeUsuarios(ItensDePacotesDeUsuarios ItensDePacotesDeUsuarios)
        {
            try
            {
                _HojeEuCasoDbContext.ItensDePacotesDeUsuarios.Add(ItensDePacotesDeUsuarios);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateItensDePacotesDeUsuarios(ItensDePacotesDeUsuarios ItensDePacotesDeUsuarios)
        {
            try
            {
                _HojeEuCasoDbContext.ItensDePacotesDeUsuarios.Update(ItensDePacotesDeUsuarios);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteItensDePacotesDeUsuarios(int ID)
        {
            try
            {
                var ItensDePacotesDeUsuarios = GetItensDePacotesDeUsuariosById(ID);
                _HojeEuCasoDbContext.ItensDePacotesDeUsuarios.Remove(ItensDePacotesDeUsuarios);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
