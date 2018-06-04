namespace WasterDAL.Repositories
{
    using System;

    using WasterDAL.Model;

    public class BatchLogRepository : BaseRepositry<BatchLog, long>, IBatchLogRepository
    {
        public BatchLogRepository(WasteContext context)
            : base(context)
        {
        }

        protected override long GenerateId()
        {
            return -1;
            //throw new NotImplementedException();
        }
    }
}