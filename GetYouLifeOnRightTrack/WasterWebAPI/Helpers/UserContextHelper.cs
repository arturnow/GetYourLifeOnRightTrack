using System;
using System.Threading;

namespace WasterWebAPI.Handlers
{
    public static class UserContextHelper
    {
        public static string SetUserContext()
        {
            //TODO: Get Login ID for filter       
            if (Thread.CurrentPrincipal == null || !Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                throw new ApplicationException("Unauthorize access");
            }
            var login = Thread.CurrentPrincipal.Identity.Name;
            return login.ToLower();
        }
        
    }
}