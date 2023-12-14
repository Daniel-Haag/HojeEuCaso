using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HojeEuCaso.Models
{
    public class Pacote
    {
        public int PacoteID { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        public decimal ReajusteAnualPorcentagem { get; set; }
        public decimal DescontoSegundaFeira { get; set; }
        public decimal DescontoTercaFeira { get; set; }
        public decimal DescontoQuartaFeira { get; set; }
        public decimal DescontoQuintaFeira { get; set; }
        public decimal DescontoSextaFeira { get; set; }
        public decimal DescontoSabado { get; set; }
        public decimal DescontoDomingo { get; set; }
        public int QtdMaximaEventosDia { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public int? FornecedorID { get; set; } //Pode ou não ter um fornecedor
        public Fornecedor Fornecedor { get; set; }
        public bool Ativo { get; set; }
        public Cidade Cidade { get; set; }
        public Estado Estado { get; set; }
        public Pais Pais { get; set; }
        public string CaminhoFoto { get; set; }
        public string CaminhoVideo { get; set; }
    }
}
