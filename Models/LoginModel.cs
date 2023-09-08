using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HojeEuCaso.Models
{
    public class LoginModel
    {
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
