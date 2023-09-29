using System;

namespace HojeEuCaso.Models
{
    public class FornecedorIndicado
    {
        public int FornecedorIndicadoID { get; set; }
        public int FornecedorID { get; set; }
        public Fornecedor FornecedorPai { get; set; }
        public string NomeFornecedor { get; set; }
        public decimal TotalAReceber { get; set; }
    }
}
