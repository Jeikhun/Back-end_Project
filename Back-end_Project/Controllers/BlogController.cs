using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Controllers
{
    public class BlogController : Controller
    {
        [Authorize]   
        public IActionResult Index()
        {
            return View();
        }
    }
}
