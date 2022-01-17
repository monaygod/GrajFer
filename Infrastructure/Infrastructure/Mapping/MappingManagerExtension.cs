using System;
using AutoMapper;
using Infrastructure.DDD.Interface;

namespace Infrastructure.Mapping
{
    /// <summary>
    /// Automaper wymusza dziedziczenie Profile w klasie, standardowa implementacja tymczasowo tutaj
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbObject"></typeparam>
    public static class MappingManagerExtension
    {
        public static TEntity MapToDDD<TEntity>(IMapper mapper, MappingManager mappingManager, object entity)
            where TEntity : DDDBuildingBlock
        {
            var mappedDDD = mapper.Map<TEntity>(entity);
            mappingManager.AddRelation(mappedDDD, entity);
            return mappedDDD;
        }

        public static TDbObject MapToDbObject<TDbObject>(IMapper mapper, 
            MappingManager mappingManager,
            DDDBuildingBlock entity, 
            Func<DDDBuildingBlock, bool> equalityComparer = null)
        {
            if (entity is not DDDBuildingBlock)
            {
                throw new ArgumentException($"{nameof(entity)} is not ${nameof(DDDBuildingBlock)} type");
            }

            if (mappingManager.MappingExist(entity,equalityComparer))
            {
                var fromContext = mappingManager.GetRelation<TDbObject>(entity, equalityComparer);
                mapper.Map(entity, fromContext);
                return fromContext;
            }

            return mapper.Map<TDbObject>(entity);
        }
    }
}