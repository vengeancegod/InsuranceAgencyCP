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
using Microsoft.AspNetCore.Authorization;

namespace InsuranceAgency__.Controllers
{
    public class PersonalInfoesController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<PersonalInfoesController> _logger;

        public PersonalInfoesController(InsuranceAgencyDataContext context, ILogger<PersonalInfoesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: PersonalInfoes
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер персональных данных запущен!");
            }
            catch
            {
                _logger.LogError("Контроллер персональных данных не был запущен. Вызвано исключение!");
            }
            return View(await _context.Personalinfos.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int PassportNumber)
        {
            var perinfo = _context.Personalinfos.ToList().Where(p => p.PassportNumber == PassportNumber);
            return View(perinfo);
        }
        // GET: PersonalInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInfo = await _context.Personalinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            return View(personalInfo);
        }

        // GET: PersonalInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO,BirthDate,PassportNumber,PassportSeries,Region,City,Address,PhoneNumber")] PersonalInfo personalInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Персональные данные успешно добавлены!");
                    _context.Add(personalInfo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Персональные данные не были добавлены. Вызвано исключение!");
            }
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInfo = await _context.Personalinfos.FindAsync(id);
            if (personalInfo == null)
            {
                return NotFound();
            }
            return View(personalInfo);
        }

        // POST: PersonalInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,BirthDate,PassportNumber,PassportSeries,Region,City,Address,PhoneNumber,ClientId")] PersonalInfo personalInfo)
        {
            if (id != personalInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Персональные данных были успешно изменены!");
                    _context.Update(personalInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Пероснальные данные не были изменены. Вызвано исключение!");
                    if (!PersonalInfoExists(personalInfo.Id))
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
            return View(personalInfo);
        }

        // GET: PersonalInfoes/Delete/5
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInfo = await _context.Personalinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            return View(personalInfo);
        }

        // POST: PersonalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var personalInfo = await _context.Personalinfos.FindAsync(id);
                _context.Personalinfos.Remove(personalInfo);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Персональные данные были успешно удалены !");
            }
            catch (Exception)
            {
                _logger.LogError("Персональные данные не были удалены. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInfoExists(int id)
        {
            return _context.Personalinfos.Any(e => e.Id == id);
        }
    }
}
