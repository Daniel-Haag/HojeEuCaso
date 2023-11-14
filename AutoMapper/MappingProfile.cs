using AutoMapper;
using HojeEuCaso.Dtos;
using HojeEuCaso.Models;

namespace HojeEuCaso.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PacoteComItensDoPacoteDto, Pacote>();
            CreateMap<PacoteComItensDoPacoteDto, ItensDePacotes>();
            CreateMap<FornecedorDto, Fornecedor>();
        }
    }
}
