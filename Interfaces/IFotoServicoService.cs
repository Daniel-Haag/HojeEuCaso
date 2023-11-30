using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IFotoServicoService
    {
        public List<FotosServicos> GetAllFotoServicos();
        public FotosServicos GetFotoServicoById(int ID);
        public List<FotosServicos> GetFotosServicoByServiceId(int ID);
        public void CreateNewFotoServico(FotosServicos FotoServico);
        public void UpdateFotoServico(FotosServicos FotoServico);
        public void DeleteFotoServico(int ID);
    }
}
