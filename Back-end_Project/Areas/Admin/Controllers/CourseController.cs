using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly EHDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public CourseController(EHDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            var courses = _dbContext.courses.Where(x => !x.IsDeleted).ToList();
            return View(courses);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!course.FormFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }
            course.Image = course.FormFile.CreateImage(_env.WebRootPath, "assets/img/");

            course.CreatedTime = DateTime.Now;
            await _dbContext.courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();


            return RedirectToAction("index", "course");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Course? course = await _dbContext.courses.FindAsync(id);
            course.IsDeleted = true;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Course? course = _dbContext.courses
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Course course, int id)
        {
            if (!ModelState.IsValid) return View(course);
            var exCourse = await _dbContext.courses.FindAsync(id);
            if (exCourse == null) { return NotFound(); }
            if (course.FormFile != null)
            {

                exCourse.Image = course.FormFile
                        .CreateImage(_env.WebRootPath, "assets/img/");
            }
            exCourse.Title = course.Title;
            exCourse.Text = course.Text;
            course.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "course");
        }
    }
}

