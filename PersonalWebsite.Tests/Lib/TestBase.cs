using System;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Abstract;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Media;
using WhileLearningCzech.Domain.Services.Users;
using WhileLearningCzech.Domain.Services.WordGroups;
using WhileLearningCzech.Domain.Services.Words;

namespace PersonalWebsite.Tests.Lib
{
    public abstract class TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;

        public TestBase()
        {
            _services = SetupDependencies();
            _serviceProvider = _services.BuildServiceProvider();
        }

        protected void CleanDatabase()
        {
            var db = GetService<LearningDbContext>();

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
            where TEntity: class, IEntity
        {
            var db = GetService<LearningDbContext>();
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        private IServiceCollection SetupDependencies()
        {
            ServiceCollection services = new ServiceCollection();
            string connectionString =
                @"Server=DESKTOP-D0NSBJ1\SQLEXPRESS;Database=PersonalWebsiteDb_test;Trusted_Connection=True;MultipleActiveResultSets=true";
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
