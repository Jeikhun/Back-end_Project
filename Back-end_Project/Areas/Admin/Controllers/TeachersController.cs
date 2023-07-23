using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class TeachersController : Controller
    {
        private readonly EHDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public TeachersController(EHDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Networks = _dbContext.networks.ToList();
            ViewBag.Hobbies = _dbContext.hobbies.ToList();
            var teachers = await _dbContext.teachers.Where(x => !x.IsDeleted).
                Include(x => x.TeacherHobbies).
                ToListAsync();
            return View(teachers);
        }
        public IActionResult Create()
        
        {
            ViewBag.Networks = _dbContext.networks.Where(x => !x.IsDeleted).ToList();
            ViewBag.Hobbies = _dbContext.hobbies.Where(x=> !x.IsDeleted).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher,IFormFile file)
        {
            teacher.FormFile = file;
            ViewBag.Networks = _dbContext.networks.Where(x=> !x.IsDeleted).ToList();
            ViewBag.Hobbies = _dbContext.hobbies.Where(x => !x.IsDeleted).ToList();


            if (!ModelState.IsValid)
            {
                return View(teacher);
            }


            if (!teacher.FormFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }


             teacher.Image = teacher.FormFile.CreateImage(_env.WebRootPath, "assets/img/");

            foreach (var item in teacher.HobbyIds)
            {
                if (!await _dbContext.hobbies.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "-----");
                    return View(teacher);
                }
                TeacherHobbies teacherHobbies = new TeacherHobbies
                {
                    HobbyId = item,
                    Teacher = teacher,
                    CreatedTime = DateTime.Now
                };
                await _dbContext.teacherHobbies.AddAsync(teacherHobbies);

            }

            teacher.CreatedTime = DateTime.Now;
            await _dbContext.teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();


            return RedirectToAction("index", "teachers");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher? teacher = await _dbContext.teachers.FindAsync(id);
            teacher.IsDeleted = true;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Teacher? teacher = _dbContext.teachers
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Teacher teacher, int id)
        {
            if (!ModelState.IsValid) return View(teacher);
            var exTeacher = await _dbContext.teachers.FindAsync(id);
            if (exTeacher == null) { return NotFound(); }
            if (teacher.FormFile != null)
            {

                exTeacher.Image = teacher.FormFile
                        .CreateImage(_env.WebRootPath, "assets/img/");
            }
            exTeacher.Phone = teacher.Phone;
            exTeacher.Faculty = teacher.Faculty;
            exTeacher.Speciality = teacher.Speciality;
            exTeacher.About = teacher.About;
            exTeacher.Degree = teacher.Degree;
            exTeacher.Email = teacher.Email;
            exTeacher.Experience = teacher.Experience;
            exTeacher.Fullname = teacher.Fullname;
            exTeacher.HobbyIds = teacher.HobbyIds;
            exTeacher.Networks = teacher.Networks;
            exTeacher.Skype = teacher.Skype;
            exTeacher.IsDeleted = teacher.IsDeleted;
            teacher.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "teachers");
        }
    }
}

