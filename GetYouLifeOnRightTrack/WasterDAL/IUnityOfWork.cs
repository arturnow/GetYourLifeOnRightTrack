using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasterDAL
{
    using System.Data.Entity;

    public interface IUnityOfWork : IDisposable
    {
        void Save();
    }

    public class WasterUnitOfWork : IUnityOfWork
    {
        private readonly DbContext _context;

        public WasterUnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
