using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DDD.Interface;

namespace Infrastructure.Mapping
{
    public class MappingManager
    {
        private readonly Dictionary<DDDBuildingBlock, List<object>> _DDDToEFRelations;

        public MappingManager()
        {
            _DDDToEFRelations = new Dictionary<DDDBuildingBlock, List<object>>();
        }

        public void AddRelation(DDDBuildingBlock DDDObject, object DBObject)
        {
            if (_DDDToEFRelations.TryGetValue(DDDObject, out var dbObjectList))
            {
                dbObjectList.Add(DBObject);
            }
            else
            {
                _DDDToEFRelations.Add(DDDObject, new List<object>() {DBObject});
            }
        }

        public void AddRelation(DDDBuildingBlock DDDObject, params object[] DBObject)
        {
            if (_DDDToEFRelations.TryGetValue(DDDObject, out var dbObjectList))
            {
                dbObjectList.AddRange(DBObject);
            }
            else
            {
                _DDDToEFRelations.Add(DDDObject, DBObject.ToList());
            }
        }

        public List<object> GetRelations(DDDBuildingBlock DDDObject)
        {
            if (_DDDToEFRelations.TryGetValue(DDDObject, out var dbObjectList))
            {
                return dbObjectList;
            }

            return null;
        }

        public T GetRelation<T>(DDDBuildingBlock DDDObject)
        {
            if (_DDDToEFRelations.TryGetValue(DDDObject, out var dbObjectList))
            {
                return (T) dbObjectList.FirstOrDefault(x => x.GetType() == typeof(T));
            }

            return default;
        }

        public T GetRelation<T>(DDDBuildingBlock DDDObject,
            Func<DDDBuildingBlock, bool> equalityComparer)
        {
            if (equalityComparer == null)
            {
                return GetRelation<T>(DDDObject);
            }

            var entity = _DDDToEFRelations //Trochę proteza
                .Where(x => x.Key.GetType() == DDDObject.GetType())
                .FirstOrDefault(x =>  equalityComparer(x.Key));

            return (T) entity.Value.FirstOrDefault(x => x.GetType() == typeof(T));
        }

        private bool MappingExist(DDDBuildingBlock DDDObject)
        {
            return _DDDToEFRelations.ContainsKey(DDDObject);
        }

        public bool MappingExist(DDDBuildingBlock DDDObject,
            Func<DDDBuildingBlock, bool> equalityComparer = null)
        {
            if (equalityComparer == null)
            {
                return MappingExist(DDDObject);
            }

            return _DDDToEFRelations //Trochę proteza
                .Select(x => x.Key)
                .Where(x => x.GetType() == DDDObject.GetType())
                .FirstOrDefault(equalityComparer) != null;
        }
    }
}