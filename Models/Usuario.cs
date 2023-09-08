using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HojeEuCaso.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public int RoleID { get; set; } 
        public Role Role { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
