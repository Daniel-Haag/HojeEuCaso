using System;

namespace HojeEuCaso.Models
{
    public class ItensDePacotes
    {
        public int ItensDePacotesID { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public int PacoteID { get; set; }
        public Pacote Pacote { get; set; }
    }
}
