using MediatR;

namespace TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RefreshToken
{
    public record RefreshTokenCommand(string Token, string IpAddress) : IRequest<AuthenticateResponse>;
}