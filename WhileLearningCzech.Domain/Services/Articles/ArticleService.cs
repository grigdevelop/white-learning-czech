using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Helpers.Exceptions;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.Articles.Dto;
using WhileLearningCzech.Domain.Services.Media;

namespace WhileLearningCzech.Domain.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly LearningDbContext _db;
        private readonly IHtmlImagesService _htmlImagesService;

        public ArticleService(LearningDbContext db, IHtmlImagesService htmlImagesService)
        {
            _db = db;
            _htmlImagesService = htmlImagesService;
        }

        public async Task<ArticleDto[]> Search()
        {
            var query = _db.Articles.AsQueryable();

            return (await query.ToListAsync())
                .ToEntityDtoList<ArticleDto, Article>()
                .ToArray();
        }

        public async Task<ArticleDto> Create(ArticleDto article)
        {
            if(await _db.Articles.AnyAsync(x => x.Title == article.Title))
                throw new ApiException("Article with this title already exists");

            article.Content = _htmlImagesService.ParseHtmlImages(article.Content);
            article.DatePublished = DateTime.UtcNow;

            var entity = article.ToEntity<Article, ArticleDto>();
            await _db.Articles.AddAsync(entity);
            await _db.SaveChangesAsync();

            return entity.ToEntityDto<ArticleDto, Article>();
        }

        public async Task<ArticleDto> Update(ArticleDto article)
        {
            var entity = await _db.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
            if(entity == null)
                throw new ApiException("Article not found");

            article.Content = _htmlImagesService.ParseHtmlImages(article.Content);


            entity.Title = article.Title;
            entity.Content = article.Content;
            entity.DatePublished = DateTime.UtcNow;
            _db.Articles.Update(entity);
            await _db.SaveChangesAsync();

            return entity.ToEntityDto<ArticleDto, Article>();
        }

        public async Task<ArticleDto> Delete(ArticleDto article)
        {
            var entity = await _db.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
            if (entity == null)
                throw new ApiException("Article not found");

            _db.Articles.Remove(entity);
            await _db.SaveChangesAsync();

            return entity.ToEntityDto<ArticleDto, Article>();
        }

        public async Task<ArticleDto> GetById(ArticleDto article)
        {
            return (await _db.Articles.FirstOrDefaultAsync(x => x.Id == article.Id))
                .ToEntityDto<ArticleDto, Article>();
        }
    }
}
