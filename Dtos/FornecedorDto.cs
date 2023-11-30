using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HojeEuCaso.Dtos
{
    public class FornecedorDto : IFotoDto
    {
        public int FornecedorID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Status { get; set; }
        public string Tipo { get; set; }
        public bool SolicitouExclusao { get; set; }
        public string MotivoExclusao { get; set; }
        public int IDIdentificacao { get; set; }
        public string Avaliacao { get; set; }
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
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public string TipoConta { get; set; }
        public string ResponsavelConta { get; set; }
        public string CPFCNPJResponsavelConta { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
        public string NomeResponsavel { get; set; }
        public int TelefoneResponsavel { get; set; }
        public bool AceitaMultiplosEventos { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public decimal DisponivelParaRecebimento { get; set; }
        public decimal TotalRecebido { get; set; }
        public decimal TotalAReceber { get; set; }
        public string CaminhoFoto { get; set; }
        public List<ClausulaContrato> ClausulasContrato { get; set; }
        public IFormFile Foto { get; set; }
        public List<IFormFile> Fotos { get; set; }
    }
}
