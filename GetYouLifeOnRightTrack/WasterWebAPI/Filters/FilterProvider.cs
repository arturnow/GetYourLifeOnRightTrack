using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WasterWebAPI.Filters
{
    using Microsoft.Practices.Unity;
    using System.Web.Mvc;

    public class FilterProvider : IFilterProvider
    {
        private IUnityContainer container;

        public FilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (IActionFilter actionFilter in this.container.ResolveAll<IActionFilter>())
            {
                yield return new Filter(actionFilter, FilterScope.First, null);
            }
        }
    }



}

namespace WasterWebAPI.WebAPI.Filters
{
    using Microsoft.Practices.Unity;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;



    public class FIlterProvider : IFilterProvider
    {

        private IUnityContainer container;

        public FIlterProvider(IUnityContainer container)
        {
            this.container = container;
        }


        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            throw new NotImplementedException();
        }
    }
}
