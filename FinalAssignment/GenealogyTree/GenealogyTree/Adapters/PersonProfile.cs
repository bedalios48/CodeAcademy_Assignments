using AutoMapper;
using GenealogyTree.Domain.Enums;
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
                .ForMember(p => p.DateOfBirth, opt => opt.MapFrom(c => GetDateOfBirth(c.createPerson.DateOfBirth)))
                .ForMember(p => p.Sex, opt => opt.MapFrom(c => string.IsNullOrEmpty(c.createPerson.Sex) ? ESex.Other
                : (ESex)Enum.Parse(typeof(ESex), c.createPerson.Sex)));
            CreateMap<Person, PersonResponse>()
                .ForMember(p => p.PersonId, opt => opt.MapFrom(p => p.Id));
        }

        private DateTime? GetDateOfBirth(string date) => DateTime.TryParse(date, out var parsedDate) ? parsedDate : null;
    }
}
