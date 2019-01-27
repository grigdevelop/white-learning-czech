using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FluentAssertions;
using WhileLearningCzech.Domain.Core.Abstract;

namespace PersonalWebsite.Tests.Lib
{
    public class EntityTestValidator<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _table;
        private readonly DbContext _db;

        public EntityTestValidator(DbContext dbContext)
        {
            _table = dbContext.Set<TEntity>();
            _db = dbContext;
        }

        public EntityTestValidator<TEntity> ShouldContains(Func<TEntity, bool> predicate)
        {
            if (!_table.Any(predicate))
                throw new UnitTestItemNotExistsException();

            return this;
        }

        public EntityTestValidator<TEntity> CountShouldBe(int count, Func<TEntity, bool> predicate = null)
        {
            if (predicate == null) predicate = entity => true;
            _table.Count(predicate).Should().Be(count);

            return this;
        }

        public EntityTestValidator<TEntity> ShouldNotContains(Func<TEntity, bool> predicate)
        {
            if (_table.Any(predicate))
                throw new UnitTestItemFoundException();

            return this;
        }

        public EntityTestValidator<TEntity> WeAlreadyHave(TEntity entity)
        {
            _table.Add(entity);
            _db.SaveChanges();
            return this;
        }
    }

    public class UnitTestItemNotExistsException : UnitTestAssertException
    {

    }

    public class UnitTestItemFoundException : UnitTestAssertException
    {

    }
}
