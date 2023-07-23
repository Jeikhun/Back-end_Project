using Back_end_Project.context;
using Back_end_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Controllers
{
    public class AboutController : Controller
    {
        private readonly EHDbContext _context;

        public AboutController(EHDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new TeacherVM();
            model.teachers = _context.teachers.Where(x=>!x.IsDeleted).ToList();
            model.networks = _context.networks.Where(x => !x.IsDeleted).ToList();
            return View(model);
        }
    }
}
