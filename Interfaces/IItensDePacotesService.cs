using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using System.Collections.Generic;

namespace HojeEuCaso.Interfaces
{
    public interface IItensDePacotesService
    {
        public List<ItensDePacotes> GetAllItensDePacotes();
        public ItensDePacotes GetItensDePacotesById(int ID);
        public List<ItensDePacotes> GetItensDePacotesByPacoteId(int ID);
        public void CreateNewItensDePacotes(ItensDePacotes ItensDePacotes);
        public void UpdateItensDePacotes(ItensDePacotes ItensDePacotes);
        public void DeleteItensDePacotes(int ID);
    }
}
