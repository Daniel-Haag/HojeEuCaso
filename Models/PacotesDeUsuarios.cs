using System;

namespace HojeEuCaso.Models
{
    public class PacotesDeUsuarios
    {
        public int PacotesDeUsuariosID { get; set; }
        public int FornecedorID { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public int QtdMaximaDeEventosDia { get; set; }
        public int QtdMaximaDePessoas { get; set; }
    }
}
