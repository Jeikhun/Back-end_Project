using Back_end_Project.context;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly EHDbContext _context;
        private IWebHostEnvironment _env;
        public TeacherController(EHDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Teacher> teachers = _context.teachers.Where(x=>!x.IsDeleted)
                .Include(x=>x.TeacherHobbies)
                .ThenInclude(x=>x.Hobby)
                .ToList();

            return View(teachers);
        }
    }
}
