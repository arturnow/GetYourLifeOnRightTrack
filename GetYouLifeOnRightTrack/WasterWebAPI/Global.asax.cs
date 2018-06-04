using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WasterWebAPI
{
    using WasterDAL;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //this.BeginRequest += WebApiApplication_BeginRequest;
            //this.EndRequest += WebApiApplication_EndRequest;

            AreaRegistration.RegisterAllAreas();
            //UnityConfig.RegisterComponents();  

            Bootstrapper.Initialise();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        void WebApiApplication_EndRequest(object sender, EventArgs e)
        {
            //TODO: Create per Request UnitOfWork - open context
        }


        void WebApiApplication_BeginRequest(object sender, EventArgs e)
        {
            //TODO: Create per Request UnitOfWork - close/flush
        }


    }
}