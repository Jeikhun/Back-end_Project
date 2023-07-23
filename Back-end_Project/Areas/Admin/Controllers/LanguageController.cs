using Back_end_Project.context;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class LanguageController : Controller
    {
        private readonly EHDbContext _context;

        public LanguageController(EHDbContext context)
        {
            _context = context;
        }
        public  IActionResult Index()
        {
            var languages = _context.Languages.Where(x => !x.IsDeleted).ToList();
            return View(languages);
        }
        [HttpGet]
        public  IActionResult Create()
        
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language language)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            language.CreatedTime = DateTime.Now;
            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
