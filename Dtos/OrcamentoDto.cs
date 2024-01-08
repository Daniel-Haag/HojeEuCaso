using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HojeEuCaso.Dtos
{
    public class OrcamentoDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Status { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
        public int EstadoID { get; set; }
        public Estado Estado { get; set; }
        public int CidadeID { get; set; }
        public Cidade Cidade { get; set; }
        public int CNPJ { get; set; }
        public bool AceitaMultiplosEventos { get; set; }
        public List<int> CategoriasSelecionadas { get; set; }
        public decimal DisponivelParaRecebimento { get; set; }
        public decimal TotalRecebido { get; set; }
        public decimal TotalAReceber { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public int QtdMaximaEventosDia { get; set; }
        public List<Categoria> CategoriasDisponiveis { get; set; }
        public DateTime DataCasamento { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Orcamento { get; set; }
        public string OrcamentoTexto { get; set; }
    }
}
