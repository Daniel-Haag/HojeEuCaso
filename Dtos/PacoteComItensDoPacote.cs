using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using HojeEuCaso.Interfaces;

namespace HojeEuCaso.Dtos
{
    public class PacoteComItensDoPacoteDto : IFotoDto
    {
        public int PacoteID { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
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
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
        public int EstadoID { get; set; }
        public Estado Estado { get; set; }
        public int CidadeID { get; set; }
        public Cidade Cidade { get; set; }
        public bool Ativo { get; set; }
        public List<ItensDePacotes> ItensDePacotes { get; set; }
        public List<IFormFile> Fotos { get; set; }
        public IFormFile Video { get; set; }
        public string CaminhoFoto { get; set; }
        public string CaminhoVideo { get; set; }
    }
}
