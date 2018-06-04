using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasterDAL.Model
{
    public class BatchLog : AuditableEntity<long>
    {
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool InProgress { get; set; }

    }
}
