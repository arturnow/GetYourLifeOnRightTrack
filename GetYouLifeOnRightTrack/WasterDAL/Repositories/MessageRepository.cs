using System;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class MessageRepository : BaseRepositry<Message, Guid> , IMessageRepository
    {
        public MessageRepository(WasteContext context): base(context)
        {
            
        }

        protected override Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}