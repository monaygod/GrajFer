using System;
using System.IO;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Microsoft.AspNetCore.Http;

namespace Service.GameRepository.Domain.GameFileAggregate
{
    public class GameFile : Entity, IAggregateRoot
    {
        private static string _fileFolder = "./Files/";
        
        private string _gameName;
        private string _filePath;
        private GameFile() { }
        public string GameName => _gameName;
        public string FilePath => _filePath;
        
        public static GameFile Create(Guid id, string gameName, IFormFile file)
        {
            var filePath = _fileFolder + file.FileName;
            using FileStream fileStream = File.Create(filePath);
            file.CopyTo(fileStream);
            return new GameFile
            {
                Id = id,
                _gameName = gameName,
                _filePath = filePath
            };
        }
    }
}