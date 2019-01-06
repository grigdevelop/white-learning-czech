using System.Threading.Tasks;
using WhileLearningCzech.Domain.Services.Articles.Dto;

namespace WhileLearningCzech.Domain.Services.Articles
{
    public interface IArticleService
    {
        Task<ArticleDto[]> Search();

        Task<ArticleDto> Create(ArticleDto article);

        Task<ArticleDto> Update(ArticleDto article);

        Task<ArticleDto> Delete(ArticleDto article);

        Task<ArticleDto> GetById(ArticleDto article);
    }
}
