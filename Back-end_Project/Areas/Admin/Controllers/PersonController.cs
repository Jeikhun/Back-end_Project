using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PersonController : Controller
    {
        private readonly EHDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public PersonController(EHDbContext dbContext, IWebHostEnvironment env)
        {

            _dbContext = dbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Person> people = _dbContext.people.Where(x => !x.IsDeleted).ToList();
            return View(people);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!person.FormFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }
            person.Image = person.FormFile.CreateImage(_env.WebRootPath, "assets/img/");

            person.CreatedTime = DateTime.Now;
            await _dbContext.people.AddAsync(person);
            await _dbContext.SaveChangesAsync();


            return RedirectToAction("index", "person");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Person? person = await _dbContext.people.FindAsync(id);
            person.IsDeleted = true;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Person? person = _dbContext.people
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Person person, int id)
        {
            if (!ModelState.IsValid) return View(person);
            var exPerson = await _dbContext.people.FindAsync(id);
            if (exPerson == null) { return NotFound(); }
            if (person.FormFile != null)
            {

                exPerson.Image = person.FormFile
                        .CreateImage(_env.WebRootPath, "assets/img/");
            }
            exPerson.Name = person.Name;
            exPerson.Position = person.Position;
            exPerson.Title = person.Title;
            exPerson.Text = person.Text;
            person.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "person");
        }
    }
}
