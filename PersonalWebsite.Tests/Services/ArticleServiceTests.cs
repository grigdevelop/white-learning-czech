using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalWebsite.Domain.Core.Data;
using PersonalWebsite.Tests.Lib;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Articles.Dto;

namespace PersonalWebsite.Tests.Services
{
    [TestClass]
    public class ArticleServiceTests
    {
        [TestClass]
        public class ArticleCrud : TestBase
        {
            [TestInitialize]
            public void Init()
            {
                CleanDatabase();
            }

            [TestMethod]
            public async Task ShouldCreateArticle()
            {
                var service = GetService<IArticleService>();
                var dateNow = DateTime.UtcNow;
                var article = new ArticleDto
                {
                    Title = "this is test article",
                    Content = "this is content of the test article",
                    DatePublished = dateNow
                };
                var newArticle = await service.Create(article);

                newArticle.Should().NotBeNull();
                newArticle.Title.Should().Be(article.Title);
                newArticle.Content.Should().Be(article.Content);
                newArticle.Id.Should().NotBe(0);

                ForIt<Article>().ShouldContains(x =>
                    x.Content == article.Content &&
                    x.Title == article.Title &&
                    x.Id == newArticle.Id);
            }

            [TestMethod]
            public async Task ShouldUpdateArticle()
            {
                var article = new Article
                {
                    Title = "my title",
                    Content = "interesting content",
                    DatePublished = DateTime.UtcNow
                };
                ForIt<Article>().WeAlreadyHave(article);

                var service = GetService<IArticleService>();
                var updatedArticle = article.ToEntityDto<ArticleDto, Article>();
                updatedArticle.Title = "new title";
                await service.Update(updatedArticle);

                ForIt<Article>().ShouldContains(x =>
                    x.Id == article.Id &&
                    x.Title == updatedArticle.Title);
            }

            [TestMethod]
            public async Task ShouldDeleteArticle()
            {
                var article = new Article
                {
                    Title = "my title",
                    Content = "interesting content",
                    DatePublished = DateTime.UtcNow
                };
                ForIt<Article>().WeAlreadyHave(article);

                var service = GetService<IArticleService>();
                await service.Delete(new ArticleDto {Id = article.Id});

                ForIt<Article>().ShouldNotContains(x => x.Id == article.Id);
            }
        }


        [TestClass]
        public class ArticleImages : TestBase
        {
            [TestInitialize]
            public void Init()
            {
                CleanDatabase();
            }


            [TestMethod]
            public async Task ShouldAddImages()
            {
                var article = new ArticleDto
                {
                    Title = "Here is the title",
                    DatePublished = DateTime.UtcNow
                };
                article.Content = File.ReadAllText("TestData/Content_With_New_Images.html");

                var service = GetService<IArticleService>();
                article = await service.Create(article);

                ForIt<Image>().ShouldContains(x => x.ArticleId == article.Id)
                    .CountShouldBe(2);
            }

            [TestMethod]
            public async Task ShouldUpdateImages()
            {
                // this part already tested upper
                var article = new ArticleDto
                {
                    Title = "Here is the title",
                    DatePublished = DateTime.UtcNow
                };
                article.Content = File.ReadAllText("TestData/Content_With_New_Images.html");
                var service = GetService<IArticleService>();
                article = await service.Create(article);

                // updating images
                throw new NotImplementedException();

            }

            [TestMethod]
            public void ShouldDeleteImages()
            {
                throw new NotImplementedException();
            }
        }

    }
}
