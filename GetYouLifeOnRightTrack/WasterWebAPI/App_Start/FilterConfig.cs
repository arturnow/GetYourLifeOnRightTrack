using System.Web;
using System.Web.Mvc;
using WasterWebAPI.Filters;

namespace WasterWebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new UserDoesNotExistsExceptionFilter());
        }
    }
}