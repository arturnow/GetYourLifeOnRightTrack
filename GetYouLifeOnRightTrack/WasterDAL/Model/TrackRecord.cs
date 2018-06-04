using System;

namespace WasterDAL.Model
{
    public class TrackRecord : AuditableEntity<Guid>
    {
        public string PatternValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsWorking { get; set; }

        public bool IsCancelled { get; set; }

        public int LoginId { get; set; }

        public Pattern Pattern { get; set; }
        
        public Guid PatternId { get; set; }

        public Identity Identity { get; set; }

    }
}