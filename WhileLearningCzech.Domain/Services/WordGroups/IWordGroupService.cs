using System.Threading.Tasks;
using WhileLearningCzech.Domain.Services.WordGroups.Dto;

namespace WhileLearningCzech.Domain.Services.WordGroups
{
    public interface IWordGroupService
    {
        Task<WordGroupDto[]> GetWordGroups();
        Task<WordGroupDto> CreateWordGroup(WordGroupDto wordGroup);
        Task<WordGroupDto> UpdateWordGroup(WordGroupDto wordGroup);
        Task<WordGroupDto> DeleteWordGroup(WordGroupDto wordGroup);
    }
}
