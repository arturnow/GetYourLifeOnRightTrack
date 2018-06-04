using System;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class EFGoalRepository : BaseRepositry<Goal, Guid>, IGoalRepository
    {

        public EFGoalRepository(WasteContext context): base(context)
        {
        }

        protected override Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}