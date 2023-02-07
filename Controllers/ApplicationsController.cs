using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using InsuranceAgency__.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace InsuranceAgency__.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<ApplicationsController> _logger;
        public ApplicationsController(InsuranceAgencyDataContext context, ILogger<ApplicationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер заявок успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Контроллер заявок не был запущен. Вызвано исключение!");
            }
            return View(await _context.Applicationss.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int ClientId)
        {
            var application = _context.Applicationss.ToList().Where(p => p.ClientId == ClientId);
            return View(application);
        }
        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applicationss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO,PassportNumber,PassportSeries,ContractValidity,InsuranceType,InsuranceAmount,ApplicationStatus")] Applications applications)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Заявка успешно создана!");
                    _context.Add(applications);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Заявка не была создана. Вызвано исключение!");
            }
            return View(applications);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applicationss.FindAsync(id);
            if (applications == null)
            {
                return NotFound();
            }
            return View(applications);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,PassportNumber,PassportSeries,ContractValidity,InsuranceType,InsuranceAmount,ApplicationStatus,ClientId")] Applications applications)
        {
            if (id != applications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applications);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Заявка изменена успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Заявка не была изменена. Вызвано исключение!");
                    if (!ApplicationsExists(applications.Id))
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
            return View(applications);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applications = await _context.Applicationss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var applications = await _context.Applicationss.FindAsync(id);
                _context.Applicationss.Remove(applications);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Заявка успешно удалена !");
            }
            catch (Exception)
            {
                _logger.LogError("Заявка не была удалена. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationsExists(int id)
        {
            return _context.Applicationss.Any(e => e.Id == id);
        }
    }
}
