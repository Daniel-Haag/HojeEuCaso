using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IItensDePacotesDeUsuariosService
    {
        public List<ItensDePacotesDeUsuarios> GetAllItensDePacotesDeUsuarios();
        public ItensDePacotesDeUsuarios GetItensDePacotesDeUsuariosById(int ID);
        public void CreateNewItensDePacotesDeUsuarios(ItensDePacotesDeUsuarios ItensDePacotesDeUsuarios);
        public void UpdateItensDePacotesDeUsuarios(ItensDePacotesDeUsuarios ItensDePacotesDeUsuarios);
        public void DeleteItensDePacotesDeUsuarios(int ID);
    }
}
