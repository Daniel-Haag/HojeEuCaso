using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HojeEuCaso.Models
{
    public class Plano
    {
        public int PlanoID { get; set; }
        public string Titulo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        public string PeriodoRenovacao { get; set; }
    }
}