using Back_end_Project.context;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Back_end_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NetworkController : Controller
    {
        private readonly EHDbContext _context;
        public NetworkController(EHDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

			ViewBag.Teachers = _context.teachers.ToList();
			var network = _context.networks.Where(x => !x.IsDeleted)
                .Include(x=> x.Teacher)
                .ToList();
            return View(network);
        }
        [HttpGet]
        public IActionResult Create()
        {
			ViewBag.Teachers = _context.teachers.ToList();


			return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Networks networks)
        {
			ViewBag.Teachers = _context.teachers.ToList();

			if (!ModelState.IsValid)
            {
                return View();
            }
            networks.CreatedTime = DateTime.Now;
            await _context.networks.AddAsync(networks);
            await _context.SaveChangesAsync();


            return RedirectToAction("index", "network");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Networks? networks = await _context.networks.FindAsync(id);
            networks.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Networks? networks = _context.networks
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (networks == null)
            {
                return NotFound();
            }
            return View(networks);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Networks networks, int id)
        {
            if (!ModelState.IsValid) return View(networks);
            var exNetworks = await _context.networks.FindAsync(id);
            if (exNetworks == null) { return NotFound(); }

            exNetworks.Teacher = networks.Teacher;
            exNetworks.TeacherId = networks.TeacherId;
            exNetworks.Icon = networks.Icon;
            exNetworks.Link = networks.Link;
            networks.UpdatedTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "network");
        }
    }
}
