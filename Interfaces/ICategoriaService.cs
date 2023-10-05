using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface ICategoriaService
    {
        public List<Categoria> GetAllCategorias();
        public Categoria GetCategoriaById(int ID);
        public void CreateNewCategoria(Categoria Categoria);
        public void UpdateCategoria(Categoria Categoria);
        public void DeleteCategoria(int ID);
    }
}
