using System;

namespace HojeEuCaso.Models
{
    public class FotosServicos
    {
        public int FotosServicosID { get; set; }
        public int PacoteID { get; set; }
        public Pacote Pacote { get; set; }
        public string CaminhoFoto { get; set; }
    }
}
