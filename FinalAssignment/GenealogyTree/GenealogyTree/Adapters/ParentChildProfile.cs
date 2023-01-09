using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class ParentChildProfile : Profile
    {
        public ParentChildProfile()
        {
            CreateMap<RelationRequest, ParentChild>();
        }
    }
}
