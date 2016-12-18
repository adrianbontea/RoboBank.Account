using System.Net.Http;
using System.Web.Http.Filters;
using RoboBank.Account.Application.Adapters;

namespace RoboBank.Account.Service.Custom
{
    public class SaveUnitOfWorkChangesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var unitOfWork =
                actionExecutedContext.Request.GetDependencyScope().GetService(typeof (UnitOfWork)) as UnitOfWork;

            unitOfWork?.SaveChanges();
        }
    }
}