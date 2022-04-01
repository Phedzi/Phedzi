using AutoMapper;
using Models.Checker;

namespace Models.Common
{
    public class Transformer : Profile
    {
        public Transformer()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
