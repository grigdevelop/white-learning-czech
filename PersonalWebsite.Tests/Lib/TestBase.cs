using System;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Abstract;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Media;
using WhileLearningCzech.Domain.Services.Users;
using WhileLearningCzech.Domain.Services.WordGroups;
using WhileLearningCzech.Domain.Services.Words;

namespace PersonalWebsite.Tests.Lib
{        
    [TestClass]
    public abstract class TestBase
    {
        private static IServiceCollection _services;
        private static IServiceProvider _serviceProvider;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {            
            _services = SetupDependencies();
            _serviceProvider = _services.BuildServiceProvider();
            AutoMapperExtensions.Configure();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _serviceProvider.GetService<LearningDbContext>().Dispose();
        }

        protected void CleanDatabase()
        {
            var db = GetService<LearningDbContext>();

            db.Images.RemoveRange(db.Images);
            db.SaveChanges();

            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();

            db.Users.RemoveRange(db.Users);
            db.SaveChanges();
        }

        protected TService GetService<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }

        protected void AddEntityItem<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var db = GetService<LearningDbContext>();
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        protected EntityTestValidator<TEntity> ForIt<TEntity>()
            where TEntity : class, IEntity
        {
            return new EntityTestValidator<TEntity>(GetService<LearningDbContext>());
        }

        private static IServiceCollection SetupDependencies()
        {
            ServiceCollection services = new ServiceCollection();
            string connectionString =
                @"Server=DESKTOP-D0NSBJ1\SQLEXPRESS;Database=PersonalWebsiteDb_Test;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<LearningDbContext>(options => options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWordGroupService, WordGroupService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IImagesService, ImagesService>();
            return services;
        }
    }
}
