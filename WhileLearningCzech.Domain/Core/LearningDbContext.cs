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

        public LearningDbContext(DbContextOptions options)
            :base(options)
        {
            
        }
    }
}
