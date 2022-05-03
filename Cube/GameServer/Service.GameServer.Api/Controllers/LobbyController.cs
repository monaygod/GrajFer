using System.Threading.Tasks;
using Infrastructure.Auth.JwtUtils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.GameServer.Application.Commands.CreateRoom;
using Service.GameServer.Application.Commands.JoinRoom;
using Service.GameServer.Application.Commands.LeaveRoom;
using Service.GameServer.Application.Queries.GetRoomList;

namespace Service.GameServer.Api.Controllers
{
    [ApiController]
    [Route("api/game/lobby/[action]")]
    [AuthorizeRoute]
    public class LobbyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LobbyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomList(GetRoomListQuery command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> JoinRoom(JoinRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LeaveRoom(LeaveRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}