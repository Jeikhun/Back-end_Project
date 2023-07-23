using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            
            ViewBag.CourseAssests = _dbContext.cAssets.Where(x => !x.IsDeleted).ToList();
            var courses = _dbContext.courses.Where(x => !x.IsDeleted).ToList();
            return View(courses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Languages = _dbContext.Languages.Where(x=>!x.IsDeleted).ToList();
            ViewBag.Tags = _dbContext.Tags.Where(x=>!x.IsDeleted).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x=>!x.IsDeleted).ToList();
            ViewBag.CourseAssests = _dbContext.cAssets.Where(x => !x.IsDeleted).ToList();


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            ViewBag.Languages = _dbContext.Languages.Where(x => !x.IsDeleted).ToList();
            ViewBag.Tags = _dbContext.Tags.Where(x => !x.IsDeleted).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x => !x.IsDeleted).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!course.FormFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }
            course.Image = course.FormFile.CreateImage(_env.WebRootPath, "assets/img/");

            foreach (var item in course.CategoryIds)
            {
                if (!await _dbContext.Categories.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "-----");
                    return View(course);
                }
                CourseCategory courseCategories = new CourseCategory
                {
                    CategoryId = item,
                    Course = course,
                    
                    CreatedTime = DateTime.Now
                };
                await _dbContext.courseCategories.AddAsync(courseCategories);

            }
            foreach (var item in course.TagIds)
            {
                if (!await _dbContext.Tags.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "-----");
                    return View(course);
                }
                CourseTag courseTags = new CourseTag
                {
                    CourseId = item,
                    Course = course,
                    CreatedTime = DateTime.Now
                };
                await _dbContext.courseTags.AddAsync(courseTags);

            }

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
            exCourse.Name = course.Name;
            exCourse.Description = course.Description;
            course.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "course");
        }
    }
}

