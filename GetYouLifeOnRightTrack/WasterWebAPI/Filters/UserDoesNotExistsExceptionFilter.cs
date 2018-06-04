using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common.Exceptions;

namespace WasterWebAPI.Filters
{
    public class UserDoesNotExistsExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception as UserDoesNotExistsException;

            if (exception != null)
            {
                FormsAuthentication.SignOut();
            }

        }
    }
}