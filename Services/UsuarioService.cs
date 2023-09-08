using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;

namespace HojeEuCaso.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HojeEuCasoDbContext _HojeEuCasoDbContext;

        public UsuarioService(HojeEuCasoDbContext HojeEuCasoDbContext) 
        {
            _HojeEuCasoDbContext = HojeEuCasoDbContext;
        }

        public void CreateNewUser(Usuario usuario)
        {
            try
            {
                _HojeEuCasoDbContext.Usuarios.Add(usuario);
                _HojeEuCasoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                string erro = e.Message;
                throw new Exception("Erro no cadastro de usuário");
            }
        }
    }
}
