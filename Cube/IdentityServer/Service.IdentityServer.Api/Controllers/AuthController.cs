using System.Threading.Tasks;
using Infrastructure.Auth.JwtUtils;
using Service.IdentityServer.Application.UserAggregate.Login;
using Service.IdentityServer.Application.UserAggregate.RefreshAccessToken;
using Service.IdentityServer.Application.UserAggregate.RevokeToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Service.IdentityServer.Api.Controllers
{
    [ApiController]
    [Route("api/auth/[action]")]
    [AuthorizeRoute]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login to APP
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Login to APP</returns>
        /// <response code="200">Request has succeeded</response>
        /// <response code="401">Request has not been applied because it lacks valid authentication credentials for the target resource</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginCommandResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var response = await _mediator.Send(command);
            if (response != null) return Ok(response);
            return Unauthorized();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshAccessTokenCommandResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenCommand command)
        {
            var response = await _mediator.Send(command);
            if (response != null) return Ok(response);
            return Unauthorized();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RevokeToken(RevokeTokenCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}