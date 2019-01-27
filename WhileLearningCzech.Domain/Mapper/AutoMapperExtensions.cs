using System.Collections.Generic;
using PersonalWebsite.Domain.Core.Data;
using WhileLearningCzech.Domain.Core.Abstract;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Services.Users.Dto;
using WhileLearningCzech.Domain.Services.WordGroups.Dto;
using WhileLearningCzech.Domain.Services.Words.Dto;

namespace WhileLearningCzech.Domain.Mapper
{
    public static class AutoMapperExtensions
    {
        public static TEntity ToEntity<TEntity, TEntityDto>(this TEntityDto entityDto)
            where TEntity : class, IEntity
            where TEntityDto : class, IEntityDto
        {
            if (entityDto == null) return null;

            return AutoMapper.Mapper.Map<TEntity>(entityDto);
        }

        public static TEntityDto ToEntityDto<TEntityDto, TEntity>(this TEntity entity)
            where TEntity : class, IEntity
            where TEntityDto : class, IEntityDto
        {
            if (entity == null) return null;

            return AutoMapper.Mapper.Map<TEntityDto>(entity);
        }

        public static IEnumerable<TEntity> ToEntityList<TEntity, TEntityDto>(this IEnumerable<TEntityDto> entityDto)
            where TEntity : class, IEntity
            where TEntityDto : class, IEntityDto
        {
            if (entityDto == null) return null;

            return AutoMapper.Mapper.Map<IEnumerable<TEntity>>(entityDto);
        }

        public static IEnumerable<TEntityDto> ToEntityDtoList<TEntityDto, TEntity>(this IEnumerable<TEntity> entity)
            where TEntity : class, IEntity
            where TEntityDto : class, IEntityDto
        {
            if (entity == null) return null;

            return AutoMapper.Mapper.Map<IEnumerable<TEntityDto>>(entity);
        }

        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<WordGroup, WordGroupDto>();
                cfg.CreateMap<WordGroupDto, WordGroup>();

                cfg.CreateMap<Word, WordDto>();
                cfg.CreateMap<WordDto, Word>();

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });
        }
    }
}
