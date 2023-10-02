using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface ICidadeService
    {
        public List<Cidade> GetAllCidades();
        public Cidade GetCidadeById(int ID);
        public void CreateNewCidade(Cidade cidade);
        public void UpdateCidade(Cidade cidade);
        public void DeleteCidade(int ID);
    }
}
