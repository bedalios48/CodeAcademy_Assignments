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
        }
    }
}
