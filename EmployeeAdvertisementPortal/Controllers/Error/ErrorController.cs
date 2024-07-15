using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.Controllers.Error
{
    public class ErrorController : Controller
    {
        public ActionResult BadRequest()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult InternalError()
        {
            return View();
        }
    }
}