using System;
using System.Threading;

namespace WasterLOB
{
    public class ThreadUserContextProvider : IUserContextProvider
    {
        public string GetContextUserName()
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