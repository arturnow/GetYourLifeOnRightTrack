using System;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class EFMessageRepository : BaseRepositry<Message, Guid>, IMessageRepository
    {
        public EFMessageRepository(WasteContext context)
            : base(context)
        {
            
        }

        protected override Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}