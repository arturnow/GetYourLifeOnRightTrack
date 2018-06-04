using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WasterWebAPI
{
    using Microsoft.Practices.Unity;
    using System.Web.Http.Dependencies;

    public class WebApiUnityDependencyResolver : IDependencyResolver
    {
        protected IUnityContainer Container;

        public WebApiUnityDependencyResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.Container = container;
        }
        public void Dispose()
        {
            this.Container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = this.Container.CreateChildContainer();
            return new WebApiUnityDependencyResolver(child);
        }

        //public IDependencyScope BeginScope()
        //{
        //    var child = container.CreateChildContainer();
        //    return new UnityResolver(child);
        //}
    }
}