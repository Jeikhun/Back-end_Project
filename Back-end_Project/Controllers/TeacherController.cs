using Back_end_Project.context;
using Back_end_Project.Models;
using Back_end_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end_Project.Controllers
{
    public class TeacherController : Controller
    {
        private readonly EHDbContext _context;

        public TeacherController(EHDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new TeacherVM();
            model.networks = _context.networks.Where(x=>!x.IsDeleted).ToList();
            model.teachers = _context.teachers.Where(x=>!x.IsDeleted).ToList();
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _context.teachers
                .Include(x => x.Networks.Where(x=>!x.IsDeleted))
                .Include(x => x.TeacherHobbies)
                .ThenInclude(x => x.Hobby)
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            return View(teacher);
        }
    }
}
