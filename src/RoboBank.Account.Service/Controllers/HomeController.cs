using System.Web.Mvc;

namespace RoboBank.Account.Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}
