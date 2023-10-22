using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;

namespace HojeEuCaso.Interfaces
{
    public interface ILoginService
    {
        public void RemoveSession(HttpContext HttpContext);
        public void AddSession(HttpContext HttpContext, UsuarioSistema usuarioSistema);
        public void AddSession(HttpContext httpContext, Fornecedor fornecedor);
    }
}
