using System;

namespace HojeEuCaso.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }
        public string UF { get; set; }
        public string Nome { get; set; }
        public int? PaisID { get; set; }
        public Pais? Pais { get; set; }
    }
}
