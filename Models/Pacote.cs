using System;

namespace HojeEuCaso.Models
{
    public class Pacote
    {
        public int PacoteID { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public decimal Preco { get; set; }
        public int QtdMaximaEventosDia { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public int? FornecedorID { get; set; } //Pode ou não ter um fornecedor
        public Fornecedor Fornecedor { get; set; }
        public bool Ativo { get; set; }
        public Cidade Cidade { get; set; }
        public Estado Estado { get; set; }
    }
}
