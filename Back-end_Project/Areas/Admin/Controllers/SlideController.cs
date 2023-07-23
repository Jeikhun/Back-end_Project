using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class SlideController : Controller
    {
        private readonly EHDbContext _eHDbContext;
        private readonly IWebHostEnvironment _env;

        public SlideController(EHDbContext eHDbContext, IWebHostEnvironment env)
        {
            _eHDbContext = eHDbContext;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var slides =  _eHDbContext.slides.Where(x=> x.IsDeleted==false).ToList();
            return View(slides);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var slides = _eHDbContext.slides.Where(x => x.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!slide.formFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }
            slide.Image = slide.formFile.CreateImage(_env.WebRootPath, "assets/img/");
            slide.CreatedTime = DateTime.Now;
            await _eHDbContext.slides.AddAsync(slide);
            await _eHDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Slide? slide = await _eHDbContext.slides.FindAsync(id);
            slide.IsDeleted = true;
            _eHDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Slide? slide = _eHDbContext.slides
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Slide slide, int id)
        {
            if (!ModelState.IsValid) return View(slide);
            Slide? exSlide = _eHDbContext.slides.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (exSlide == null) { return NotFound(); }
            if (slide.formFile != null)
            {

                exSlide.Image = slide.formFile
                        .CreateImage(_env.WebRootPath, "assets/img/");
            }
            slide.UpdatedTime = DateTime.Now;
            await _eHDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
