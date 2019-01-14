using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WhileLearningCzech.Domain.Core.Data;

namespace WhileLearningCzech.Domain.Core
{
    public class LearningDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<WordGroup> WordGroups { get; set; }

        public DbSet<Word> Words { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Image> Images { get; set; }

        public LearningDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ImageMapper());
        }
    }
}
