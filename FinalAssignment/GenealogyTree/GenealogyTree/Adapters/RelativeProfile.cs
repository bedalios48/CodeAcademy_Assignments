using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class RelativeProfile : Profile
    {
        public RelativeProfile()
        {
            CreateMap<Relative, RelativeResponse>()
                .ForMember(r => r.PersonId, opt => opt.MapFrom(r => r.Person.Id))
                .ForMember(r => r.NameSurname, opt => opt.MapFrom(r => r.Person.Name + ' ' + r.Person.Surname))
                .ForMember(r => r.Relation, opt => opt.MapFrom(r => r.Relation));
        }
    }
}
