using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IFornecedorIndicadoService
    {
        public List<FornecedorIndicado> GetAllFornecedorIndicados();
        public FornecedorIndicado GetFornecedorIndicadoById(int ID);
        public void CreateNewFornecedorIndicado(FornecedorIndicado FornecedorIndicado);
        public void UpdateFornecedorIndicado(FornecedorIndicado FornecedorIndicado);
        public void DeleteFornecedorIndicado(int ID);
    }
}
