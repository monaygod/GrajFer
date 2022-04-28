using System.Threading.Tasks;
using Infrastructure.Auth.JwtUtils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.GameRepository.Application.Commands.UploadGame;
using Service.GameRepository.Application.Queries.DownloadGame;
using Service.GameRepository.Application.Queries.GetGamesList;

namespace Service.GameRepository.Api.Controllers
{
    [ApiController]
    [Route("api/auth/[action]")]
    [AuthorizeRoute]
    public class GameRepositoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameRepositoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadGame([FromForm] UploadGameCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DownloadGameQueryResult))]
        public async Task<IActionResult> DownloadGame(DownloadGameQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetGamesListQueryResult))]
        public async Task<IActionResult> GetGamesList(GetGamesListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}