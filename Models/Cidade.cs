using System;

namespace HojeEuCaso.Models
{
    public class Cidade
    {
        public int CidadeID { get; set; }
        public string Nome { get; set; }
        public int? EstadoID { get; set; }
        public Estado? Estado { get; set; }
    }
}
