using System;
using Infrastructure.Application.Command;
using Microsoft.AspNetCore.Http;

namespace Service.GameRepository.Application.Commands.UploadGame
{
    public class UploadGameCommand : CommandBase
    {
        public Guid Id { get; set; }
        public string GameName { get; set; }
        public IFormFile File { get; set; }
    }
}