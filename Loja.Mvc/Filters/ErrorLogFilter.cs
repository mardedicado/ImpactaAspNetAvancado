using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Filters
{
    public class ErrorLogFilter : HandleErrorAttribute              //Para herdar a classe
    {
        //Ocorrerá uma substituição
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {

                var controller = filterContext.RouteData.Values["controller"].ToString();
                var action = filterContext.RouteData.Values["action"].ToString();
                var loogerName = $"{controller}Controller.{action}"; //$"" String interpolada. Concat também concatena

                log4net.LogManager
                    .GetLogger(loogerName)
                    .Error(filterContext.Exception.Message, filterContext.Exception);

                //filterContext.

                base.OnException(filterContext);

            }

        }
        
    }
}