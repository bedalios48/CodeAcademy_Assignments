using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<CreatePersonRequest, Person>();
            CreateMap<Person, PersonResponse>()
                .ForMember(p => p.PersonId, opt => opt.MapFrom(p => p.Id));
        }
    }
}
