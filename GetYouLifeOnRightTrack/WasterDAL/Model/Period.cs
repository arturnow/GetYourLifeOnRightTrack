using System;

namespace WasterDAL.Model
{
    public class Period : AuditableEntity<long>
    {
        public PeriodType Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Duration { get; set; }
    }
}