using System;

namespace WasterDAL.Model
{
    public class Message : AuditableEntity<Guid>// BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string Url { get; set; }

        public DateTime DueDate { get; set; }

        public Identity Identity { get; set; }

        public int IdentityId { get; set; }

        public string Type { get; set; }

        public bool RequireConfirmation { get; set; }
        public bool IsSent { get; set; }

    }
}