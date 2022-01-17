using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Application.Command.Interface;

namespace Infrastructure.Application.Command
{
    public class CommandResultBase : ICommandResultBase
    {
        private Dictionary<Guid, object> Values { get; set; }

        public CommandResultBase()
        {
            Values ??= new Dictionary<Guid, object>();
        }
        public void AddValue(Guid id, object value)
        {
            if (value != null)
                Values.Add(id, value);
        }

        public T GetValueById<T>(Guid id)
        {
            var obj = Values.FirstOrDefault(x => x.Key == id).Value;
            if (obj is T Tobj)
                return Tobj;
            return default;
        }
    }
}