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
    public class UsuarioSistemaService : IUsuarioSistemaService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public UsuarioSistemaService(HojeEuCasoDbContext HojeEuCasoDbContext)
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public List<UsuarioSistema> GetAllUsuarioSistema()
        {
            return _HojeEuCasoDbContext.UsuariosSistema
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .ToList();
        }

        public UsuarioSistema GetUsuarioSistemaById(int ID)
        {
            return _HojeEuCasoDbContext.UsuariosSistema.Where(x => x.UsuarioSistemaID == ID).FirstOrDefault();
        }

        public void CreateNewUsuarioSistema(UsuarioSistema UsuarioSistema)
        {
            try
            {
                _HojeEuCasoDbContext.UsuariosSistema.Add(UsuarioSistema);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro no cadastro!");
            }
        }

        public void UpdateUsuarioSistema(UsuarioSistema UsuarioSistema)
        {
            try
            {
                _HojeEuCasoDbContext.UsuariosSistema.Update(UsuarioSistema);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na atualização deste registro");
            }
        }

        public void DeleteUsuarioSistema(int ID)
        {
            try
            {
                var UsuarioSistema = GetUsuarioSistemaById(ID);
                _HojeEuCasoDbContext.UsuariosSistema.Remove(UsuarioSistema);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro na exclusão deste registro");
            }
        }
    }
}
