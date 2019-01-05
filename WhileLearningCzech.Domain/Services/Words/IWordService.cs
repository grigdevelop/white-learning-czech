using System.Threading.Tasks;
using WhileLearningCzech.Domain.Services.Words.Dto;

namespace WhileLearningCzech.Domain.Services.Words
{
    public interface IWordService
    {
        Task<WordDto[]> Search(SearchWordDto input);

        Task<WordDto> CreateWord(WordDto word);

        Task<WordDto> UpdateWord(WordDto word);

        Task<WordDto> DeleteWord(WordDto word);

        Task<WordDto> GetById(WordDto word);
    }
}
