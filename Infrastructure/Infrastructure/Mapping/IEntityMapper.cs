using System;
using Infrastructure.DDD.Interface;

namespace Infrastructure.Mapping
{
    public interface IEntityMapper<TEntity, TDbObject> where TEntity : DDDBuildingBlock 
    {
        public abstract TEntity MapToDDD(TDbObject entity);
        public abstract TDbObject MapToDbObject(TEntity entity, Func<DDDBuildingBlock, bool> equalityComparer = null);
    }
}