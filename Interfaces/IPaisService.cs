using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IPaisService
    {
        public List<Pais> GetAllPaises();
        public Pais GetPaisById(int ID);
        public void CreateNewPais(Pais pais);
        public void UpdatePais(Pais pais);
        public void DeletePais(int ID);
    }
}
