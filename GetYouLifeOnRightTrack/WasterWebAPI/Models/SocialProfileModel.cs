using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WasterWebAPI.Models
{
    public class SocialProfileModel
    {
        public long Id { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

    }
}