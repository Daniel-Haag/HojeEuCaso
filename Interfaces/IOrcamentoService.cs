using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IOrcamentoService
    {
        public List<Orcamento> GetAllOrcamentos();
        public Orcamento GetOrcamentoById(int ID);
        public void CreateNewOrcamento(Orcamento Orcamento);
        public void UpdateOrcamento(Orcamento Orcamento);
        public void DeleteOrcamento(int ID);
    }
}
