using System;

namespace HojeEuCaso.Models
{
    public class Pacote
    {
        public int PacoteID { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public int QtdMaximaEventosDia { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public int? FornecedorID { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
