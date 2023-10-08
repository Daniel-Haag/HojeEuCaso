using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IPacoteService
    {
        public List<Pacote> GetAllPacotes();
        public Pacote GetPacoteById(int ID);
        public void CreateNewPacote(Pacote Pacote);
        public void UpdatePacote(Pacote Pacote);
        public void DeletePacote(int ID);
    }
}
