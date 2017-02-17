using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AlexaSkillProject.Repository
{
    /// <summary>
    /// Abstract implementation of Genric IRepository interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class AbstractGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public AbstractGenericRepository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        
        public void UpdateEntity(TEntity entity)
        {           
            Context.Entry(entity).State = EntityState.Modified;
        }


        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public int QueryCount(Expression<Func<TEntity, Boolean>> predicate)
        {
            return Context.Set<TEntity>().Count(predicate);   
        }
    }
}
