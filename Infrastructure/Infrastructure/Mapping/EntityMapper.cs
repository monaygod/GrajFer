using System;
using AutoMapper;
using Infrastructure.DDD.Interface;

namespace Infrastructure.Mapping
 {
     /// <summary>
     /// Nie zadziała, automaper skanuje assembly i próbuje tą klasę zainicjalizować co się nie może udac bo jest generyczna
     /// </summary>
     public abstract class EntityMapper<TEntity, TDbObject> : Profile, IEntityMapper<TEntity, TDbObject> where TEntity : DDDBuildingBlock
     {
         private readonly IMapper _mapper;
         private readonly MappingManager _mappingManager;

         protected EntityMapper()
         {
             DefineMapping();
         }

         protected EntityMapper(IMapper mapper, MappingManager mappingManager)
         {
             _mapper = mapper;
             _mappingManager = mappingManager;
         }

         public virtual TEntity MapToDDD(TDbObject entity)
         {
             return MappingManagerExtension.MapToDDD<TEntity>(_mapper,_mappingManager,entity);
         }

         public virtual TDbObject MapToDbObject(TEntity entity, Func<DDDBuildingBlock, bool> equalityComparer = null)
         {
             return MappingManagerExtension.MapToDbObject<TDbObject>(_mapper, _mappingManager, entity, equalityComparer);
         }

         protected abstract void DefineMapping();
     }
 }