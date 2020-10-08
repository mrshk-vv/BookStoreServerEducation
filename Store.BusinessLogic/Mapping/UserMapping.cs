using AutoMapper;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserModel>();

            CreateMap<UserModel, User>()
                .ForMember(x => x.UserName, options => options.MapFrom(src => src.Email));

            CreateMap<UserSingUpModel, User>()
                .ForMember(x => x.UserName, options => options.MapFrom(src => src.Email));

        }

    }
}
