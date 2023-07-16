using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
