using AutoMapper;
using GenealogyTree.Domain.Models;
using GenealogyTree.DTO;

namespace GenealogyTree.Adapters
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<(RegisterRequest registerRequest, byte[] passwordHash, byte[] passwordSalt), User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(m => m.registerRequest.UserName))
                .ForMember(u => u.Role, opt => opt.MapFrom(m => m.registerRequest.Role))
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(m => m.passwordHash))
                .ForMember(u => u.PasswordSalt, opt => opt.MapFrom(m => m.passwordSalt));
        }
    }
}
