using System;
using System.Linq;
using System.Linq.Expressions;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public interface IBaseRepositry<TEntity, TKey>
        where TEntity : AuditableEntity<TKey> where TKey : struct
    {
        TEntity Get(TKey id);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, string include = null);

        TKey Create(TEntity entity);

        TKey Update(TEntity entity);

        void Delete(TKey id);
    }
}