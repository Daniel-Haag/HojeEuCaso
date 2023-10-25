using AutoMapper;
using HojeEuCaso.Dtos;
using HojeEuCaso.Models;

namespace HojeEuCaso.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PacoteComItensDoPacote, Pacote>();
            CreateMap<PacoteComItensDoPacote, ItensDePacotes>();
        }
    }
}
