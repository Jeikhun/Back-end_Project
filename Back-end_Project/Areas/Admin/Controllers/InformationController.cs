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
    public class InformationController : Controller
    {
        private readonly EHDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public InformationController(EHDbContext dbContext, IWebHostEnvironment env)
        {
            
            _dbContext = dbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List <Information> info = _dbContext.information.Where(x => !x.IsDeleted).ToList();
            return View(info);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Information? information)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!information.FormFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }
            information.Image = information.FormFile.CreateImage(_env.WebRootPath, "assets/img/");

            information.CreatedTime = DateTime.Now;
            await _dbContext.information.AddAsync(information);
            await _dbContext.SaveChangesAsync();


            return RedirectToAction("index", "information");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Information? information = await _dbContext.information.FindAsync(id);
            information.IsDeleted = true;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Information? information = _dbContext.information
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (information == null)
            {
                return NotFound();
            }
            return View(information);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Information information, int id)
        {
            if (!ModelState.IsValid) return View(information);
            var exInformation = await _dbContext.information.FindAsync(id);
            if (exInformation == null) { return NotFound(); }
            if (information.FormFile != null)
            {

                exInformation.Image = information.FormFile
                        .CreateImage(_env.WebRootPath, "assets/img/");
            }
            exInformation.Link = information.Link;
            exInformation.Title = information.Title;
            exInformation.Text = information.Text;
            information.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "information");
        }
    }
}
