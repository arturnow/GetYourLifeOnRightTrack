using System;

namespace WasterWebAPI.Models
{
    public class TrackRecordViewModel
    {
        public Guid Id { get; set; }
        public string Domain { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsWorking { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}