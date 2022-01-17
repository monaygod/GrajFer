using System.Collections.Generic;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.DDD.Interface;

namespace Infrastructure.Mapping
{
    public abstract class EntityCollectionResolverHelper<TDbObject,TEntity,TCollectionEntity> : IValueResolver<TDbObject,TEntity, ICollection<TCollectionEntity>>
        where TEntity : DDDBuildingBlock
        where TDbObject : DbBuildingBlock
        where TCollectionEntity : DDDBuildingBlock
    {
        public abstract ICollection<TCollectionEntity> Resolve(
            TDbObject source,
            TEntity destination,
            ICollection<TCollectionEntity> destMember,
            ResolutionContext context
        );
    }

    public abstract class EntityCollectionResolver<TCollectionEntity, TCollectionDbObject, TMapper, TEntity, TDbObject> :
        EntityCollectionResolverHelper<TDbObject, TEntity, TCollectionEntity>,
        IValueResolver<TEntity, TDbObject, ICollection<TCollectionDbObject>>
        where TEntity : DDDBuildingBlock
        where TDbObject : DbBuildingBlock
        where TCollectionDbObject : DbBuildingBlock
        where TCollectionEntity : DDDBuildingBlock
        where TMapper : EntityMapper<TEntity, TDbObject>
    {
        private readonly MappingManager _mappingManager;
        private readonly IMapper _mapper;

        protected EntityCollectionResolver(MappingManager mappingManager, IMapper mapper)
        {
            _mappingManager = mappingManager;
            _mapper = mapper;
        }

        protected TCollectionEntity MapToDDD(TCollectionDbObject entity)
        {
            return MappingManagerExtension.MapToDDD<TCollectionEntity>(_mapper, _mappingManager,
                entity);
        }

        protected TCollectionDbObject MapToDbObject(TCollectionEntity entity)
        {
            return MappingManagerExtension.MapToDbObject<TCollectionDbObject>(_mapper, _mappingManager,
                entity);
        }

        public abstract override ICollection<TCollectionEntity> Resolve(
            TDbObject source,
            TEntity destination,
            ICollection<TCollectionEntity> destMember,
            ResolutionContext context
        );

        public abstract ICollection<TCollectionDbObject> Resolve(
            TEntity source,
            TDbObject destination,
            ICollection<TCollectionDbObject> destMember,
            ResolutionContext context
        );
    }
}