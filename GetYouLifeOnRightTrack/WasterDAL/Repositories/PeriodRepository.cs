using System;
using System.Linq;
using System.Runtime.InteropServices;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class PeriodRepository : BaseRepositry<Period,long>, IPeriodRepository
    {
        public PeriodRepository(WasteContext context) : base(context)
        {
        }

        protected override long GenerateId()
        {
            return Query(p => true).OrderBy(p => p.Id).Select(p => p.Id).FirstOrDefault() + 1;
        }
    }
}