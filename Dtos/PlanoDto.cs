using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HojeEuCaso.Dtos
{
    public class PlanoDto
    {
        public int PlanoID { get; set; }
        public string Titulo { get; set; }
        public string Preco { get; set; }
        public string PeriodoRenovacao { get; set; }
    }
}