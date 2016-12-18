using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Elmah;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Service.Custom
{
    public class ElmahAccountExceptionHandlingAttribute : ElmahGenericExceptionHandlingAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is AccountException)
            {
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(actionExecutedContext.Exception, HttpContext.Current));

                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionExecutedContext.Exception.Message);
                return;
            }

            base.OnException(actionExecutedContext);
        }
    }
}