using System;

namespace WasterDAL.Model
{
    /// <summary>
    /// Goal we set. 
    /// </summary>
    public class Goal : AuditableEntity<Guid>// BaseEntity<Guid>
    {
        public String Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDateTime { get; set; }

        public int Progress { get; set; }

        /// <summary>
        /// Use either DueDateTime or Duration. Duration is counted from Create or Start Time
        /// </summary>
        public int Duration { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int IdentityId { get; set; }

        public Identity Identity{ get; set; }
    }
}