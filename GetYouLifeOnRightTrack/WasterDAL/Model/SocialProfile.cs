using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasterDAL.Model
{
    public class SocialProfile : AuditableEntity<long>
    {
        public int IdentityId { get; set; }
        public Identity Identity { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }

        public ProfileType Type { get; set; }
    }
}
