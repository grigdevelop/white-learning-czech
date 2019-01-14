using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Data;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Articles.Dto;
using WhileLearningCzech.Domain.Services.Media;

namespace PersonalWebsite.Tests.Services
{
    [TestClass]
    public class ArticleServiceTests
    {
        [TestMethod]
        public async Task ShouldCreateArticle()
        {
            AutoMapperExtensions.Configure();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("PersonalDb");
            LearningDbContext db = new LearningDbContext(builder.Options);
            IArticleService service = new ArticleService(db);

            string html = File.ReadAllText("TestData/ImageSources.html");

            var article = await service.Create(new ArticleDto {Title = "sometitle", Content = html});
            article = await service.Update(article);

            var ws = db.Words.ToList();
        }

        [TestMethod]
        public void Should()
        {
            string html = File.ReadAllText("TestData/ImageSources.html");
            DocumentFormatter df = new DocumentFormatter(html);
            df.GetImageSources();
        }

        [TestMethod]
        public void ShouldParseId()
        {
            var source = "/images/source/11.jpg";
            int startIndex = source.LastIndexOf('/') + 1;
            int endIndex = source.LastIndexOf('.');
            int length = endIndex - startIndex;
            var substr = source.Substring(startIndex, length);
            int id = int.Parse(substr);
        }
    }
}
