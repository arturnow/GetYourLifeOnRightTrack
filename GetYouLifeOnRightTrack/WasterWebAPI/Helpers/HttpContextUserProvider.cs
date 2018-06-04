using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WasterLOB;

namespace WasterWebAPI.Helpers
{
    public class UserHttpContextProvider : IUserContextProvider
    {
        public string GetContextUserName()
        {
            //TODO: Get Login ID for filter       
            if (HttpContext.Current == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new ApplicationException("Unauthorize access");
            }
            var login = HttpContext.Current.User.Identity.Name;
            return login.ToLower();
        }
    }
}