using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RevokeAuthentication
{
    public record RevokeAuthenticationCommand(string Token, string IpAddress) : IRequest<EmptyResult>;
}