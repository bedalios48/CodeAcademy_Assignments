using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class ParentChildProfile : Profile
    {
        public ParentChildProfile()
        {
            CreateMap<(RelationRequest relation, int userId), ParentChild>()
                .ForMember(p => p.ChildId, opt => opt.MapFrom(r => r.relation.ChildId))
                .ForMember(p => p.ParentId, opt => opt.MapFrom(r => r.relation.ParentId))
                .ForMember(p => p.ChildId, opt => opt.MapFrom(r => r.relation.ChildId));
        }
    }
}
