using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;

namespace HojeEuCaso.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioSistemaService _usuarioSistemaService;

        public LoginService(IUsuarioSistemaService usuarioSistemaService)
        {
            _usuarioSistemaService = usuarioSistemaService;
        }

        public void RemoveSession(HttpContext httpContext)
        {
            httpContext.Session.Remove("Nome");
            httpContext.Session.Remove("Role");
            httpContext.Session.Remove("Email");
            httpContext.Session.Remove("UsuarioID");
            httpContext.Session.Remove("FornecedorID");
        }

        public void AddSession(HttpContext httpContext, UsuarioSistema usuarioSistema)
        {
            //Implementar uma forma de adicionar noivo e wedManager
            httpContext.Session.SetString("Nome", usuarioSistema.Nome);
            httpContext.Session.SetString("Email", usuarioSistema.Email);
            httpContext.Session.SetString("UsuarioID", usuarioSistema.UsuarioSistemaID.ToString());

            var usuario = _usuarioSistemaService.GetUsuarioSistemaById(usuarioSistema.UsuarioSistemaID);

            httpContext.Session.SetString("Role", usuario.Role);
        }

        public void AddSession(HttpContext httpContext, Fornecedor fornecedor)
        {
            httpContext.Session.SetString("Nome", fornecedor.Nome);
            httpContext.Session.SetString("Email", fornecedor.Email);
            httpContext.Session.SetString("FornecedorID", fornecedor.FornecedorID.ToString());
            httpContext.Session.SetString("Role", "Fornecedor");
        }
    }
}
