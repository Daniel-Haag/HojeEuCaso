using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IPlanoFornecedorService
    {
        public List<PlanoFornecedor> GetAllPlanosFornecedores();
        public PlanoFornecedor GetPlanoFornecedorById(int ID);
        public void CreateNewPlanoFornecedor(PlanoFornecedor PlanoFornecedor);
        public void UpdatePlanoFornecedor(PlanoFornecedor PlanoFornecedor);
        public void DeletePlanoFornecedor(int ID);
        public void AtualizaStatusPlanoFornecedorConformeAsaas();
    }
}
