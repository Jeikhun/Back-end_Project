using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HobbyController : Controller
    {
        private readonly EHDbContext _context;
        public HobbyController(EHDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var hobbies = _context.hobbies.Where(x=>!x.IsDeleted).ToList();
            return View(hobbies);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            hobby.CreatedTime = DateTime.Now;
            await _context.hobbies.AddAsync(hobby);
            await _context.SaveChangesAsync();


            return RedirectToAction("index", "hobby");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Hobby? hobby = await _context.hobbies.FindAsync(id);
            hobby.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Hobby? hobby = _context.hobbies
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (hobby == null)
            {
                return NotFound();
            }
            return View(hobby);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Hobby hobby, int id)
        {
            if (!ModelState.IsValid) return View(hobby);
            var exHobby = await _context.hobbies.FindAsync(id);
            if (exHobby == null) { return NotFound(); }
            
            exHobby.Name = hobby.Name;
            exHobby.TeacherHobbies = hobby.TeacherHobbies;
            hobby.UpdatedTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "hobby");
        }
    }
}
