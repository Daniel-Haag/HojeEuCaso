using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IEstadoService
    {
        public List<Estado> GetAllEstados();
        public Estado GetEstadoById(int ID);
        public void CreateNewEstado(Estado Estado);
        public void UpdateEstado(Estado Estado);
        public void DeleteEstado(int ID);
    }
}
