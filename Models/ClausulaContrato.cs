using System;

namespace HojeEuCaso.Models
{
    public class ClausulaContrato
    {
        public int ClausulaContratoID { get; set; }
        public string Descricao { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
