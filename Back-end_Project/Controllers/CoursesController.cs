using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
