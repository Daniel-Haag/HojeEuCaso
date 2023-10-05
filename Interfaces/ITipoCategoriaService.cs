using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface ITipoCategoriaService
    {
        public List<TipoCategoria> GetAllTipoCategorias();
        public TipoCategoria GetTipoCategoriaById(int ID);
        public void CreateNewTipoCategoria(TipoCategoria TipoCategoria);
        public void UpdateTipoCategoria(TipoCategoria TipoCategoria);
        public void DeleteTipoCategoria(int ID);
    }
}
