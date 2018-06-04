using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    //TODO: Just temporary

    public abstract class BaseRepositry<TEntity, TKey> : IBaseRepositry<TEntity, TKey> , IDisposable
        where TEntity : AuditableEntity<TKey>
        where TKey : struct,  IEquatable<TKey>
    {
        private DbContext _context;

        public ILogger Logger { get; set; }

        protected BaseRepositry(WasteContext context)
        {
            //TODO: To dopracowaæ~!
            _context = context;
            _context.Database.Log = s => Debug.Write(s); //Logging to Output
        }
        public TEntity Get(TKey id)
        {
            return _context.Set<TEntity>().FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, string include = null)
        {
            if (!string.IsNullOrWhiteSpace(include))
            {
                return _context.Set<TEntity>().Where(filter).Include(include);
                
            }
            return _context.Set<TEntity>().Where(filter);
        }

        public TKey Create(TEntity entity)
        {
            entity.Id = this.GenerateId();
            entity.CreateDate = DateTime.Now;

            this._context.Set<TEntity>().Add(entity);


            this._context.SaveChanges();

            return entity.Id;
        }

        public TKey Update(TEntity entity)
        {
            var existingEntity = this.Get(entity.Id);

            if (existingEntity == null)
            {
                throw new ApplicationException(string.Format("Couldn't find entity of type {0} with id = {1}", typeof(TEntity).Name, entity.Id));
            }
            
            MapForUpdate(entity, existingEntity);

            existingEntity.UpdateDate = DateTime.Now;

            _context.SaveChanges();

            return entity.Id;
        }


        public virtual void Delete(TKey id)
        {
            var existingEntity = this.Get(id);

            if (existingEntity == null)
            {
                throw new ApplicationException(string.Format("Couldn't find entity of type {0} with id = {1}", typeof(TEntity).Name, id));
            }

            _context.Set<TEntity>().Remove(existingEntity);
        }


        //TODO: Move to service or let DB generate ID

        [Obsolete("should be in service?")]
        protected abstract TKey GenerateId();
        [Obsolete("Should I move it to service? Use Automapper also?")]
        private TEntity MapForUpdate(TEntity update, TEntity existing)
        {
            foreach (var propertyInfo in update.GetType().GetProperties())
            {

                if (propertyInfo.Name == "Id" || propertyInfo.Name == "CreateDate" || propertyInfo.Name == "UpdateDate" || propertyInfo.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute)) != null || (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String"))
                    continue;
                propertyInfo.SetValue(existing, propertyInfo.GetValue(update));

            }

            return existing;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}