using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HojeEuCaso.Models
{
    public class Orcamento
    {
        public int OrcamentoID { get; set; }
        public int UsuarioSistemaID { get; set; }
        public UsuarioSistema WedManager { get; set; }
        public string servicosIDs { get; set; }
        public DateTime DataEvento { get; set; }
        public int QtdConvidados { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotalOrcamento { get; set; }

    }
}
