using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface ICategoriasDosPlanosService
    {
        public List<CategoriaDosPlanos> GetAllCategoriasDosPlanos();
        public CategoriaDosPlanos GetCategoriasDosPlanosById(int ID);
        public void CreateNewCategoriasDosPlanos(CategoriaDosPlanos CategoriasDosPlanos);
        public void UpdateCategoriasDosPlanos(CategoriaDosPlanos CategoriasDosPlanos);
        public void DeleteCategoriasDosPlanos(int ID);
    }
}
