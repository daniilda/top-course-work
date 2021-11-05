using System.Text.Json.Serialization;
using MediatR;

namespace TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RegisterUser
{
    public record RegisterUserCommand
        (string Username, string Password, bool IsExtended = false) : IRequest<AuthenticateResponse>
    {
        [JsonIgnore] 
        public string IpAddress { get; set; } = "0.0.0.0";
    }
}