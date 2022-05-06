using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Domain.Enums;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Domain.ValueObject
{
    public class Game : Infrastructure.DDD.ValueObject
    {
        public bool GameInProgress { get; private set; }
        public ICollection<StaticField> StaticFields { get; private set; }
        public Game()
        {
            GameInProgress = false;
        }
        public void StartGame(ICollection<StaticField> staticFields)
        {
            if (GameInProgress is true)
            {
                throw new BadHttpRequestException("Game already started!");
            }
            GameInProgress = true;
            StaticFields = staticFields.ToList();
        }

        public void ResetGame()
        {
            if (GameInProgress is false)
            {
                throw new BadHttpRequestException("Game not started yet!");
            }
            StaticFields = new List<StaticField>();
            GameInProgress = false;
        }

        public void MoveActiveElement(Guid sourceFieldId, Guid destinationFieldId, Guid elementId)
        {
            if (GameInProgress is false)
            {
                throw new BadHttpRequestException("Game not started yet!");
            }
            var sourceField = StaticFields.FirstOrDefault(x => x.Id == sourceFieldId);
            if (sourceField is null)
            {
                throw new BadHttpRequestException("Wrong source field id!");
            }
            var destinationField = StaticFields.FirstOrDefault(x => x.Id == destinationFieldId);
            if (destinationField is null)
            {
                throw new BadHttpRequestException("Wrong destination field id!");
            }

            var element = sourceField.GetField(elementId);
            if (destinationField is null)
            {
                throw new BadHttpRequestException("Wrong element id!");
            }
            sourceField.RemoveElement(element);
            destinationField.Add(element);
        }
        public void UsePredefinedFunction(Guid fieldId, Guid elementId, PredefinedFunction predefinedFunction)
        {
            switch (predefinedFunction)
            {
                case PredefinedFunction.ShuffleElements:
                    var field = StaticFields.FirstOrDefault(x => x.Id == fieldId);
                    if(field is not null)
                        field.Shuffle();
                    break;
            }
        }
    }
}