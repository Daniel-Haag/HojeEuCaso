using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IUsuarioSistemaService
    {
        public List<UsuarioSistema> GetAllUsuarioSistema();
        public UsuarioSistema GetUsuarioSistemaById(int ID);
        public void CreateNewUsuarioSistema(UsuarioSistema UsuarioSistema);
        public void UpdateUsuarioSistema(UsuarioSistema UsuarioSistema);
        public void DeleteUsuarioSistema(int ID);
    }
}
