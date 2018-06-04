using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterWebAPI.Handlers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Configuration;

    public class Auditor<TEntity, TKey> : IBaseRepositry<TEntity, TKey>
        where TEntity : AuditableEntity<TKey>
        where TKey : struct
    {

        private readonly IBaseRepositry<TEntity, TKey> _repository;

        public Auditor(IBaseRepositry<TEntity, TKey> repository)
        {
            this._repository = repository;
        }
        public TEntity Get(TKey id)
        {
            return this._repository.Get(id);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, string include = null)
        {
            return this._repository.Query(filter, include);
        }

        public TKey Create(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;

            return this._repository.Create(entity);
        }

        public TKey Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;

            return this._repository.Update(entity);
        }

        public void Delete(TKey id)
        {
            this._repository.Delete(id);
        }


    }
}