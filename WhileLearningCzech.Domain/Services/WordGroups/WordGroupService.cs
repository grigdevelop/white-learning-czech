using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Helpers.Exceptions;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.WordGroups.Dto;

namespace WhileLearningCzech.Domain.Services.WordGroups
{
    public class WordGroupService : IWordGroupService
    {
        private readonly LearningDbContext _db;

        public WordGroupService(LearningDbContext db)
        {
            _db = db;
        }

        public async Task<WordGroupDto[]> GetWordGroups()
        {
            return (await _db.WordGroups.ToListAsync())
                .ToEntityDtoList<WordGroupDto, WordGroup>().ToArray();
        }

        public async Task<WordGroupDto> CreateWordGroup(WordGroupDto wordGroup)
        {
            var dbWordGroup = await _db.WordGroups.FirstOrDefaultAsync(w => w.Name == wordGroup.Name);
            if(dbWordGroup != null)
                throw new ApiException("WordGroup with this name already exists");

            dbWordGroup = wordGroup.ToEntity<WordGroup, WordGroupDto>();
            _db.WordGroups.Add(dbWordGroup);
            await _db.SaveChangesAsync();

            return dbWordGroup.ToEntityDto<WordGroupDto, WordGroup>();
        }

        public async Task<WordGroupDto> UpdateWordGroup(WordGroupDto wordGroup)
        {
            var dbWordGroup = await _db.WordGroups.FirstOrDefaultAsync(x => x.Id== wordGroup.Id);
            if (dbWordGroup == null)
                throw new ApiException("WordGroup not found");
            if(await _db.WordGroups.AnyAsync(x => x.Id != wordGroup.Id && x.Name == wordGroup.Name))
                throw new ApiException("WordGroup with this name already exists");

            dbWordGroup.Name = wordGroup.Name;
            _db.WordGroups.Update(dbWordGroup);
            await _db.SaveChangesAsync();

            return dbWordGroup.ToEntityDto<WordGroupDto, WordGroup>();
        }

        public async Task<WordGroupDto> DeleteWordGroup(WordGroupDto wordGroup)
        {
            var dbWordGroup = await _db.WordGroups.FirstOrDefaultAsync(x => x.Id == wordGroup.Id);
            if (dbWordGroup == null)
                throw new ApiException("WordGroup not found");

            _db.WordGroups.Remove(dbWordGroup);
            await _db.SaveChangesAsync();
            return dbWordGroup.ToEntityDto<WordGroupDto, WordGroup>();
        }
    }
}
