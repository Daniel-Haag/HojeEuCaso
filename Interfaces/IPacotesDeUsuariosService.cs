using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IPacotesDeUsuariosService
    {
        public List<PacotesDeUsuarios> GetAllPacoteDeUsuarios();
        public PacotesDeUsuarios GetPacoteDeUsuarioById(int ID);
        public void CreateNewPacoteDeUsuario(PacotesDeUsuarios PacoteDeUsuario);
        public void UpdatePacoteDeUsuario(PacotesDeUsuarios PacoteDeUsuario);
        public void DeletePacoteDeUsuario(int ID);
    }
}
