using AutoMapper;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.Authenticate;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RefreshToken;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RevokeAuthentication;
using TopCourseWorkBl.DataLayer.Cmds.Auth;

namespace TopCourseWorkBl.MapperProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RefreshTokenCommand, GetUserByRefreshTokenCmd>()
                .ForMember(x => x.RefreshToken, opt => opt.MapFrom(y => y.Token));

            CreateMap<RefreshTokenCommand, UpdateRefreshTokenCmd>()
                .ForMember(x => x.Token, opt => opt.MapFrom(y => y.Token))
                .ForMember(x => x.RevokedByIp, opt => opt.MapFrom(y => y.IpAddress));

            CreateMap<RevokeAuthenticationCommand, UpdateRefreshTokenCmd>()
                .ForMember(x => x.RevokedByIp, opt => opt.MapFrom(y => y.IpAddress));
            
            CreateMap<AuthenticateCommand, GetUserByUsernameCmd>();
        }
    }
}