using System;

namespace WasterDAL.Model
{
    public class Pattern : AuditableEntity<Guid> //BaseEntity<Guid>
    {
        public string Value { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsWorking { get; set; }

        public int IdentityId { get; set; }

        public Identity Identity { get; set; }

    }
}