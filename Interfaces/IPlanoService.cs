using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IPlanoService
    {
        public List<Plano> GetAllPlanos();
        public Plano GetPlanoById(int ID);
        public void CreateNewPlano(Plano Plano);
        public void UpdatePlano(Plano Plano);
        public void DeletePlano(int ID);
    }
}
