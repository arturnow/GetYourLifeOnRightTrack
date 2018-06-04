using System;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class EFTaskRepositry : BaseRepositry<Task, Guid>, ITaskRepository
    {
        public EFTaskRepositry(WasteContext context)
            : base(context)
        {
            
        }

        protected override Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}