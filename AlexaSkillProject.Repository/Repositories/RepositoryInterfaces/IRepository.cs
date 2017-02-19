using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AlexaSkillProject.Repository
{

    /// <summary>
    /// Defines interface for generic repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void UpdateEntity(TEntity entity);

        int QueryCount(Expression<Func<TEntity, bool>> predicate);
        int Count();
    }
}
