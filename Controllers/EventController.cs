using Kyrsova.Data;
using Kyrsova.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kyrsova.Controllers
{
    public class EventController : Controller
    {

        private readonly WebappDbContext _context;

        public EventController(WebappDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event eventModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventModel);
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }
    }
}

    