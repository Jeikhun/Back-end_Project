using Back_end_Project.context;
using Back_end_Project.Extensions;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NoticeController : Controller
    {
        private readonly EHDbContext _dbContext;

        public NoticeController(EHDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var notices = _dbContext.notices.Where(x => !x.IsDeleted).ToList();
            return View(notices);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            notice.CreatedTime = DateTime.Now;
            await _dbContext.notices.AddAsync(notice);
            await _dbContext.SaveChangesAsync();


            return RedirectToAction("index", "notice");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Notice? notice = await _dbContext.notices.FindAsync(id);
            notice.IsDeleted = true;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Notice? notice = _dbContext.notices
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Notice notice, int id)
        {
            if(!ModelState.IsValid) return View(notice);
            var exNotice = await _dbContext.notices.FindAsync(id);
            if (exNotice == null) { return NotFound(); }
            exNotice.Link = notice.Link;
            notice.UpdatedTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "notice");
        }
    }
}
        