using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Helpers.Exceptions;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.Words.Dto;

namespace WhileLearningCzech.Domain.Services.Words
{
    public class WordService : IWordService
    {
        private readonly LearningDbContext _db;

        public WordService(LearningDbContext db)
        {
            _db = db;
        }

        public async Task<WordDto[]> Search(SearchWordDto input)
        {
            if(input == null) input = new SearchWordDto();

            var query = _db.Words.AsQueryable();

            if (input.GroupId.HasValue && input.GroupId.Value != 0)
                query = query.Where(x => x.WordGroupId == input.GroupId);

            return (await query.ToListAsync()).ToEntityDtoList<WordDto, Word>().ToArray();
        }

        public async Task<WordDto> CreateWord(WordDto word)
        {
            if(await _db.Words.AnyAsync(x => x.Czech == word.Czech))
                throw new ApiException("Word already in database");

            if(word.WordGroupId.HasValue &&
               (await _db.Words.CountAsync(x => x.WordGroupId == word.WordGroupId)) > 9)
                throw new ApiException("Max count of words in group is 10");

            var entity = word.ToEntity<Word, WordDto>();
            _db.Words.Add(entity);
            await _db.SaveChangesAsync();

            return entity.ToEntityDto<WordDto, Word>();
        }

        public async Task<WordDto> UpdateWord(WordDto word)
        {
            var entity = await _db.Words.FirstOrDefaultAsync(x => x.Id == word.Id);
            if(entity == null)
                throw new ApiException($"Word {word.Czech} not found.");

            if (word.WordGroupId.HasValue &&
                (await _db.Words.CountAsync(x => x.WordGroupId == word.WordGroupId && x.Id != word.Id)) > 9)
                throw new ApiException("Max count of words in group is 10");

            entity.Czech = word.Czech;
            entity.English = word.English;
            entity.WordGroupId = word.WordGroupId;
            _db.Words.Update(entity);
            await _db.SaveChangesAsync();

            return entity.ToEntityDto<WordDto, Word>();
        }

        public async Task<WordDto> DeleteWord(WordDto word)
        {
            var entity = await _db.Words.FirstOrDefaultAsync(x => x.Id == word.Id);
            if(entity == null)
                throw new ApiException("Word not found");

            _db.Words.Remove(entity);
            await _db.SaveChangesAsync();

            return entity.ToEntityDto<WordDto, Word>();
        }

        public async Task<WordDto> GetById(WordDto word)
        {
            var entity = await _db.Words.FirstOrDefaultAsync(x => x.Id == word.Id);
            if(entity == null)
                throw new ApiException("Word not found");

            return entity.ToEntityDto<WordDto, Word>();
        }
    }
}
