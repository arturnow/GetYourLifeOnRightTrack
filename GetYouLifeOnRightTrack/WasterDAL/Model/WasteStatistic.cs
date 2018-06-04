namespace WasterDAL.Model
{
    public class WasteStatistic : AuditableEntity<long>
    {
        public Identity Identity { get; set; }

        public int IdentityId { get; set; }
    
        public Period Period { get; set; }

        public long PeriodId { get; set; }

        public int EntryCount { get; set; }

        public long TimeSumInSecond { get; set; }

        
    }
}