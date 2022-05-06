using System.Threading.Tasks;
using Infrastructure.Auth.JwtUtils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.GameServer.Application.Commands.CreateRoom;
using Service.GameServer.Application.Commands.MoveElement;
using Service.GameServer.Application.Commands.StartGame;
using Service.GameServer.Application.Commands.UsePredefinedFunction;
using Service.GameServer.Application.Queries.GetGameState;

namespace Service.GameServer.Api.Controllers
{
    [ApiController]
    [Route("api/game/actions/[action]")]
    [AuthorizeRoute]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> StartGame(StartGameCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetGame(ResetGameCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MoveElement(MoveElementCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UsePredefinedFunction(UsePredefinedFunctionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetGameStateQueryResult))]
        public async Task<IActionResult> GetGameState(GetGameStateQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}