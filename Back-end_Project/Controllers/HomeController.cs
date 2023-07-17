using Back_end_Project.context;
using Back_end_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Back_end_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly EHDbContext _context;

        public HomeController(EHDbContext dcontext)
        {
            
            _context = dcontext;
        }
        public IActionResult Index()
        {
            var model = new ViewModel();
            model.notices = _context.notices.Where(x => !x.IsDeleted).ToList();
                model.slides = _context.slides.Where(x => !x.IsDeleted).ToList();
            model.courses = _context.courses.Where(x => !x.IsDeleted).ToList();
            model.information = _context.information.Where(x => !x.IsDeleted).ToList();
            model.people = _context.people.Where(x => !x.IsDeleted).ToList();
            return View(model);
        }
    }
}