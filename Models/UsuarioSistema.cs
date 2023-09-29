using System;

namespace HojeEuCaso.Models
{
    public class UsuarioSistema
    {
        public int UsuarioSistemaID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Status { get; set; }
        public string Tipo { get; set; }
        public bool SolicitouExclusao { get; set; }
        public string MotivoExclusao { get; set; }
        public int IDIdentificacao { get; set; }
        public string Avaliacao { get; set; }
        public DateTime DataNascimento { get; set; }
        public int RG { get; set; }
        public int CPF { get; set; }
        public int Telefone { get; set; }
        public int CEP { get; set; }
        public int EstadoID { get; set; }
        public Estado Estado { get; set; }
        public int CidadeID { get; set; }
        public Cidade Cidade { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
    }
}
