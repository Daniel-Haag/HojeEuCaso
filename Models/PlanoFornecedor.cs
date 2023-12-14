using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HojeEuCaso.Models
{
    public class PlanoFornecedor
    {
        public int PlanoFornecedorID { get; set; }
        public int FornecedorID { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int PlanoID { get; set; }
        public Plano Plano { get; set; }
        public DateTime? DataProximaRenovacao { get; set; }
        public bool Pago { get; set; }
    }
}
