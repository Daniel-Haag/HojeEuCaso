using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IPacoteService
    {
        public List<Pacote> GetAllPacotes();
        public List<Pacote> GetPacotesByCategoriaID(int categoriaID);
        public Pacote GetPacoteById(int ID);
        public Pacote GetPacoteByTitulo(string titulo);
        public List<Pacote> GetPacoteByFornecedor(int fornecedorID);
        public int CreateNewPacote(Pacote Pacote);
        public void UpdatePacote(Pacote Pacote);
        public void DeletePacote(int ID);
    }
}
