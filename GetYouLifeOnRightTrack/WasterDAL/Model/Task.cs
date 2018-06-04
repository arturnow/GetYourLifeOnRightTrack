using System;

namespace WasterDAL.Model
{
    public class Task : AuditableEntity<Guid>// BaseEntity<Guid>
    {
        /// <summary>
        ///Task must end same day (I don't mean before 24:00 if user works nights
        /// </summary>
        public int Duration { get; set; }

        public Goal Goal { get; set; }

        public Guid GoalId { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }

        public int Progress { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
    }
}