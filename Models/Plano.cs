using System;

namespace HojeEuCaso.Models
{
    public class Plano
    {
        public int PlanoID { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public string PeriodoRenovacao { get; set; }
    }
}
