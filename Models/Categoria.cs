using System;

namespace HojeEuCaso.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Descricao { get; set; }
        public int TipoCategoriaID { get; set; }
        public TipoCategoria TipoCategoria { get; set; }
    }
}
