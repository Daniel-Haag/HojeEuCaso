using System;

namespace HojeEuCaso.Models
{
    public class CategoriaDosPlanos
    {
        public int CategoriaDosPlanosID { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public int PlanoID { get; set; }
        public Plano Plano { get; set; }
    }
}
