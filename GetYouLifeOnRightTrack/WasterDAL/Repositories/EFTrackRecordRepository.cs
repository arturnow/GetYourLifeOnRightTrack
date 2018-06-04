using System;
using System.Collections.Generic;
using System.Linq;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class EFTrackRecordRepository : BaseRepositry<TrackRecord, Guid>, ITrackRecordRepository
    {

        public EFTrackRecordRepository(WasteContext context)
            : base(context)
        {
            
        }
     protected override Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}