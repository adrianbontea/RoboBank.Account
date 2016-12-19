using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using RoboBank.Account.Domain;
using Microsoft.ApplicationInsights;

namespace RoboBank.Account.Service.Custom
{
    public class AIAccountExceptionHandlingAttribute : AIGenericExceptionHandlingAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is AccountException)
            {
                var telemetryClient = new TelemetryClient();
                telemetryClient.TrackException(actionExecutedContext.Exception);

                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionExecutedContext.Exception.Message);
                return;
            }

            base.OnException(actionExecutedContext);
        }
    }
}