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

namespace InsuranceAgency__.Controllers
{
    public class AdministratorsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<AdministratorsController> _logger;

        public AdministratorsController(InsuranceAgencyDataContext context, ILogger<AdministratorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Administrators
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер администраторов успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Контроллер администраторов не был запущен. Вызвано исключение!");
            }
            return View(await _context.Administrators.ToListAsync());
        }

        // GET: Administrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // GET: Administrators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO")] Administrator administrator)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Администратор успешно добавлен!");
                    _context.Add(administrator);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Администратор не был добавлен. Вызвано исключение!");
            }
            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO")] Administrator administrator)
        {
            if (id != administrator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrator);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Данные администратора изменены успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Данные не были изменены. Вызвано исключение!");
                    if (!AdministratorExists(administrator.Id))
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
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var administrator = await _context.Administrators.FindAsync(id);
                _context.Administrators.Remove(administrator);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Администраор успешно удален !");
            }
            catch (Exception)
            {
                _logger.LogError("Администратор не был удален. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AdministratorExists(int id)
        {
            return _context.Administrators.Any(e => e.Id == id);
        }
    }
}
