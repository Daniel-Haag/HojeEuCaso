using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IFotoDto
    {
        string CaminhoFoto { get; set; }
        IFormFile Foto { get; set; }
    }
}
