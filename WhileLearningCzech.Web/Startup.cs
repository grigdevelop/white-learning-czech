using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Mapper;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Media;
using WhileLearningCzech.Domain.Services.Users;
using WhileLearningCzech.Domain.Services.WordGroups;
using WhileLearningCzech.Domain.Services.Words;
using WhileLearningCzech.Web.Helpers;
using WhileLearningCzech.Web.Services;

namespace WhileLearningCzech.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            string connectionString =
                @"Server=DESKTOP-D0NSBJ1\SQLEXPRESS;Database=LearningCzechDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<LearningDbContext>(options => options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = JwtConfigs.GetSecurityKey(),
                        ValidateAudience = false,
                        ValidateIssuer = false                       
                    };

                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";                
            });            

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWordGroupService, WordGroupService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IDirService, DirService>();
            services.AddScoped<IHtmlImagesService, HtmlImagesService>();
            services.AddScoped<IImagesService, ImagesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/content"
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";               
            });

            AutoMapperExtensions.Configure();
        }
    }
}
