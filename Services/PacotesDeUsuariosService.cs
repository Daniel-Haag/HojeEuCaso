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
    public class PacotesDeUsuariosService : IPacotesDeUsuariosService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public PacotesDeUsuariosService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<PacotesDeUsuarios> GetAllPacoteDeUsuarios()
        {
            return _HojeEuCasoDbContext.PacotesDeUsuarios
                .Include(x => x.Fornecedor)
                .ToList();
        }

        public PacotesDeUsuarios GetPacoteDeUsuarioById(int ID)
        {
            return _HojeEuCasoDbContext.PacotesDeUsuarios.Where(x => x.PacotesDeUsuariosID == ID).FirstOrDefault();
        }

        public void CreateNewPacoteDeUsuario(PacotesDeUsuarios PacoteDeUsuario)
        {
            try
            {
                _HojeEuCasoDbContext.PacotesDeUsuarios.Add(PacoteDeUsuario);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdatePacoteDeUsuario(PacotesDeUsuarios PacoteDeUsuario)
        {
            try
            {
                _HojeEuCasoDbContext.PacotesDeUsuarios.Update(PacoteDeUsuario);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeletePacoteDeUsuario(int ID)
        {
            try
            {
                var PacoteDeUsuario = GetPacoteDeUsuarioById(ID);
                _HojeEuCasoDbContext.PacotesDeUsuarios.Remove(PacoteDeUsuario);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
