using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Areas.Admin.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
