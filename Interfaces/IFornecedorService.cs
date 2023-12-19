using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IFornecedorService
    {
        public List<Fornecedor> GetAllFornecedor();
        public Fornecedor GetFornecedorById(int ID);
        public Fornecedor GetFornecedorByEmail(string email);
        public Fornecedor GetFornecedorLogin(UsuarioSistema usuarioSistema);
        public void CreateNewFornecedor(Fornecedor Fornecedor);
        public void UpdateFornecedor(Fornecedor Fornecedor);
        public void DeleteFornecedor(int ID);
    }
}
