using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using WasterDAL.Repositories;

namespace WasterWebAPI.Filters
{
    using WasterDAL;

    public class HeaderAuthorizationFilter : AuthorizationFilterAttribute
    {
        private readonly IIdentityRepository _identityRepository;

        public HeaderAuthorizationFilter()
        {
            //Need to do sth!
            _identityRepository = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IIdentityRepository)) as IIdentityRepository;
        }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext); 
            //User is authenticated
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var creds = GetBasicCredentials(authHeader.Parameter);

                    //TODO: Setup DI and server it per request or create proper factory
                    //using (var context = new WasterWebAPI.DAL.WasteContext())
                    {
                        string login = creds[0];
                        string password = creds[1];

                        //TODO: Coś z robić z UserName i Password np ustawić jakiś zmienne
                        if (creds.Length == 2 
                            && !string.IsNullOrWhiteSpace(creds[0])
                            &&!string.IsNullOrWhiteSpace(creds[1])
                            && _identityRepository.Query(identity => identity.Login == login && identity.Password == password).Any()
                            )
                        {
                            var currentPrincipal = new GenericPrincipal(new GenericIdentity(login), null);
                            Thread.CurrentPrincipal = currentPrincipal;
                            
                            FormsAuthentication.SetAuthCookie(login, false);
                            return;
                        }
                    }
                }
            }


            HandelUnauthorized(actionContext);
        }

        private void HandelUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);


            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='WasterTime' location='http://localhost:49418/api/login'");
        }

        private string[] GetBasicCredentials(string parameter)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var authenticationString = encoding.GetString(Convert.FromBase64String(parameter));

            var resultArray = authenticationString.Split(':');

            return resultArray;
        }
    }
}