using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HojeEuCaso.Dtos
{
    public class PacoteComItensDoPacote
    {
        public int PacoteID { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public decimal Preco { get; set; }
        public int QtdMaximaEventosDia { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
        public int? FornecedorID { get; set; } //Pode ou não ter um fornecedor
        public Fornecedor Fornecedor { get; set; }
        public int EstadoID { get; set; }
        public Estado Estado { get; set; }
        public int CidadeID { get; set; }
        public Cidade Cidade { get; set; }
        public bool Ativo { get; set; }
        public List<ItensDePacotes> ItensDePacotes { get; set; }
        public IFormFile Foto { get; set; }
        public IFormFile Video { get; set; }
    }
}
