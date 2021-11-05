using TopCourseWorkBl.Dtos.Auth;

namespace TopCourseWorkBl.AuthenticationLayer.Handlers.Auth
{
    public record AuthenticateResponse(User User, string JwtToken, string RefreshToken);
}