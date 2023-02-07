using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using InsuranceAgency__.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using Grpc.Core;

namespace InsuranceAgency__.Controllers
{
    public class ManagersController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<ManagersController> _logger;

        public ManagersController(InsuranceAgencyDataContext context, ILogger<ManagersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер менеджеров успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Контроллер менеджеров не был запущен. Вызвано исключение!");
            }
            return View(await _context.Managers.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string FIO)
        {
            var manager = _context.Clients.ToList().Where(p => p.FIO.StartsWith(FIO));
            return View(manager);
        }
        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO,BirthDate,Age")] Manager manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Менеджер успешно добавлен!");
                    _context.Add(manager);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Менеджер не был добавлен. Вызвано исключение!");
            }
        
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,BirthDate,Age")] Manager manager)
        {
            if (id != manager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Данные о менеджере изменены успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Данные не были изменены. Вызвано исключение!");
                    if (!ManagerExists(manager.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var manager = await _context.Managers.FindAsync(id);
                _context.Managers.Remove(manager);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Данные успешно удалены !");
            }
            catch (Exception)
            {
                _logger.LogError("Данные не были удалены. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.Id == id);
        }
    }
}
