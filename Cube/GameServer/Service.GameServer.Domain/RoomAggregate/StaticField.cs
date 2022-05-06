using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Infrastructure.DDD;
using Newtonsoft.Json;

namespace Service.GameServer.Domain.RoomAggregate;

public class StaticField : Entity
{
    public List<ActiveElement> ActiveElements { get; set; }

    public ActiveElement GetField(Guid elementId)
    {
        return ActiveElements.FirstOrDefault(x => x.Id == elementId);
    }

    public void RemoveElement(Guid elementId)
    {
        var elementToDelete = ActiveElements.FirstOrDefault(x => x.Id == elementId);
        if (elementToDelete is not null)
        {
            RemoveElement(elementToDelete);
        }
    }
    public void RemoveElement(ActiveElement elementToDelete)
    {
        ActiveElements.Remove(elementToDelete);
    }

    public ActiveElement Add(ActiveElement activeElement)
    {
        ActiveElements.Add(activeElement);
        return activeElement;
    }

    public void Shuffle()
    {
        Random rng = new Random();
        ActiveElements = ActiveElements.OrderBy(x => rng.Next())
            .ToList();
    }
    
}