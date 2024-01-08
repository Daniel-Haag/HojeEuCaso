using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HojeEuCaso.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Descricao { get; set; }
        public int TipoCategoriaID { get; set; }
        public TipoCategoria TipoCategoria { get; set; }
        [NotMapped]
        public List<Fornecedor> Fornecedores { get; set; }
    }
}
