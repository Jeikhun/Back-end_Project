using Back_end_Project.context;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        private readonly EHDbContext _context;

        public TagController(EHDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tags = _context.Tags.Where(x=>!x.IsDeleted).ToList();
            return View(tags);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            tag.CreatedTime = DateTime.Now;
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
