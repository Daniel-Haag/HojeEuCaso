using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;

namespace HojeEuCaso.Services
{
    public class LoginService : ILoginService
    {
        public LoginService() { }

        public void RemoveSession(HttpContext httpContext)
        {
            httpContext.Session.Remove("Nome");
            httpContext.Session.Remove("Role");
        }

        public void AddSession(HttpContext httpContext, UsuarioSistema usuarioSistema)
        {
            httpContext.Session.SetString("Nome", usuarioSistema.Nome);
            httpContext.Session.SetString("Email", usuarioSistema.Email);
            httpContext.Session.SetString("UsuarioID", usuarioSistema.UsuarioSistemaID.ToString());
            httpContext.Session.SetString("Role", "Usuario");
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
