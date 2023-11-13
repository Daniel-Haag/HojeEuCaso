using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IClausulaContratoService
    {
        public List<ClausulaContrato> GetAllClausulaContratos();
        public List<ClausulaContrato> GetClausulasDeContratosByFornecedorID(int ID);
        public ClausulaContrato GetClausulaContratoById(int ID);
        public void CreateNewClausulaContrato(ClausulaContrato ClausulaContrato);
        public void UpdateClausulaContrato(ClausulaContrato ClausulaContrato);
        public void DeleteClausulaContrato(int ID);
    }
}
