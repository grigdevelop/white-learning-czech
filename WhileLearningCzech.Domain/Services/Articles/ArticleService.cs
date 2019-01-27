using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PersonalWebsite.Domain.Core.Data;
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

        public ArticleService(LearningDbContext db)
        {
            _db = db;
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
            if (await _db.Articles.AnyAsync(x => x.Title == article.Title))
                throw new ApiException("Article with this title already exists");

            //article.Content = _htmlImagesService.ParseHtmlImages(article.Content);
            article.DatePublished = DateTime.UtcNow;

            var entity = article.ToEntity<Article, ArticleDto>();
            entity.Content = string.Empty; // don't save without formatting content
            await _db.Articles.AddAsync(entity);
            await _db.SaveChangesAsync();
            await UpdateArticleContent(entity, article.Content);

            return entity.ToEntityDto<ArticleDto, Article>();
        }

        public async Task<ArticleDto> Update(ArticleDto article)
        {
            var entity = await _db.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
            if (entity == null)
                throw new ApiException("Article not found");           

            entity.Title = article.Title;
            entity.Content = article.Content;
            entity.DatePublished = DateTime.UtcNow;
            await UpdateArticleContent(entity, entity.Content);
            //_db.Articles.Update(entity);
            //await _db.SaveChangesAsync();

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

        #region Utilities

        private async Task CleanArticleContentImage(Article article, DocumentFormatter df)
        {
            var articleImages = await _db.Images.Where(x => x.ArticleId == article.Id).ToListAsync();
            var htmlImagesIdList = df.GetInDocumentImagesList("api/article/image/");
            var needToDelete = articleImages.Except(htmlImagesIdList.Select(id => new Image {Id = id}))
                .ToList();
            _db.Images.RemoveRange(needToDelete);
            await _db.SaveChangesAsync();
        }

        private async Task UpdateArticleContent(Article article, string articleContent)
        {
            var df = new DocumentFormatter(articleContent);

            // clean old images
            if (article.Id != 0)
            {
                await CleanArticleContentImage(article, df);
            }

            var imageSources = df.GetImageSources();
            var images = imageSources.Select(imgSource =>
            {
                var image = new Image { ArticleId = article.Id, Data = imgSource.Data, DataType = imgSource.DataType};
                imgSource.BindEntity(image);
                return image;
            });

            await _db.Images.AddRangeAsync(images);
            await _db.SaveChangesAsync();
            imageSources.ForEach(ic => ic.UpdateHtmlSource("api/article/image/"));
            article.Content = df.Html;
            _db.Articles.Update(article);
            await _db.SaveChangesAsync();
        }      
        #endregion
    }
}
