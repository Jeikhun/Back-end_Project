using Back_end_Project.context;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CassetsController : Controller
    {
        private readonly EHDbContext _context;
        
        public CassetsController(EHDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cassets = _context.cAssets.Where(x=>!x.IsDeleted).ToList();
            return View(cassets);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CAssets cAssets)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            cAssets.CreatedTime = DateTime.Now;
            await _context.cAssets.AddAsync(cAssets);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
