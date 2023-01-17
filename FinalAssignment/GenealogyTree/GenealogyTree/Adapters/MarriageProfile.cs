using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class MarriageProfile : Profile
    {
        public MarriageProfile()
        {
            CreateMap<(MarriageRequest request, int key), Marriage>()
                .ForMember(s => s.PersonId, opt => opt.MapFrom(r => r.request.PersonId))
                .ForMember(s => s.SpouseId, opt => opt.MapFrom(r => r.request.SpouseId))
                .ForMember(s => s.AreDivorced, opt => opt.MapFrom(r => r.request.AreDivorced))
                .ForMember(s => s.MarriedDate, opt => opt.MapFrom(r => GetDate(r.request.MarriageDate)))
                .ForMember(s => s.DivorcedDate, opt => opt.MapFrom(r => GetDate(r.request.DivorceDate)))
                .ForMember(s => s.CreatedByUserId, opt => opt.MapFrom(r => r.key));
        }
        private DateTime? GetDate(string date) => DateTime.TryParse(date, out var parsedDate) ? parsedDate : null;
    }
}
