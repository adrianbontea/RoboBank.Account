using Microsoft.AspNetCore.Mvc.Filters;
using RoboBank.Account.Application.Adapters.NetStandard;

namespace RoboBank.Account.Service.NetCore.Custom
{
    public class SaveUnitOfWorkChanges : ActionFilterAttribute
    {
        private readonly UnitOfWork _unitOfWork;
        public SaveUnitOfWorkChanges(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            _unitOfWork?.SaveChanges();
        }
    }
}