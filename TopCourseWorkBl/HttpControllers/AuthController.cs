using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.AuthenticationLayer.Exceptions;
using TopCourseWorkBl.AuthenticationLayer.Extensions;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.Authenticate;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RefreshToken;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RegisterUser;
using TopCourseWorkBl.AuthenticationLayer.Handlers.Auth.RevokeAuthentication;

namespace TopCourseWorkBl.HttpControllers
{
    [ApiController]
    [Route("/v1/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
            => _mediator = mediator;
        
        [Produces("application/json")] 
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticateResponse>> Register([FromBody] RegisterUserCommand request)
        {
            try
            {
                request.IpAddress = HttpContext.GetIpAddress();
                var response = await _mediator.Send(request);
                Response.SetTokenCookie(response.RefreshToken);
                return response;
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [Produces("application/json")] 
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateCommand request)
        {
            try
            {
                request.IpAddress = HttpContext.GetIpAddress();
                var response = await _mediator.Send(request);
                Response.SetTokenCookie(response.RefreshToken);
                return response;
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [Produces("application/json")] 
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"] ?? "";
            var isExtended = !string.IsNullOrEmpty(Request.Cookies["isExtended"]);
            try
            {
                var response = await _mediator.Send(new RefreshTokenCommand(refreshToken, HttpContext.GetIpAddress()));
                Response.SetTokenCookie(response.RefreshToken, isExtended);
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpDelete("revoke")]
        public async Task<IActionResult> RevokeAuthentication()
        {
            var refreshToken = Request.Cookies["refreshToken"] ?? "";
            try
            {
                await _mediator.Send(new RevokeAuthenticationCommand(refreshToken, HttpContext.GetIpAddress()));
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}