using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;

namespace HojeEuCaso.Interfaces
{
    public interface IUsuarioService
    {
        public void CreateNewUser(Usuario usuario);
    }
}
