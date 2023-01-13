using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<(CreatePersonRequest createPerson, int userId), Person>()
                .ForMember(p => p.CreatedByUserId, opt => opt.MapFrom(c => c.userId))
                .ForMember(p => p.Name, opt => opt.MapFrom(c => c.createPerson.Name))
                .ForMember(p => p.Surname, opt => opt.MapFrom(c => c.createPerson.Surname))
                .ForMember(p => p.BirthPlace, opt => opt.MapFrom(c => c.createPerson.BirthPlace))
                .ForMember(p => p.DateOfBirth, opt => opt.MapFrom(c => c.createPerson.DateOfBirth));
            CreateMap<Person, PersonResponse>()
                .ForMember(p => p.PersonId, opt => opt.MapFrom(p => p.Id));
        }
    }
}
