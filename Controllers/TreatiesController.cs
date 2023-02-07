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
    public class TreatiesController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<TreatiesController> _logger;

        public TreatiesController(InsuranceAgencyDataContext context, ILogger<TreatiesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Treaties
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер договоров данных запущен!");
            }
            catch
            {
                _logger.LogError("Контроллер договоров не был запущен. Вызвано исключение!");
            }
            return View(await _context.Treatys.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(DateTime dateFrom, DateTime dateTo, DateTime DateOfPaymentTreaty)
        {
            var treaty = _context.Treatys.ToList().Where(p => p.DateOfPaymentTreaty >= dateFrom && p.DateOfPaymentTreaty <= dateTo);
            return View(treaty);
        }
        // GET: Treaties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treaty = await _context.Treatys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treaty == null)
            {
                return NotFound();
            }

            return View(treaty);
        }

        // GET: Treaties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Treaties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Create([Bind("Id,TypeTreaty,TypeOfInsuranceTreaty,PolicySeries,PolicyNumber,InsurancePremium,InsuranceSum,DateOfPaymentTreaty,TreatyCurrency,StateOfTreaty,IdClient")] Treaty treaty)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Договор успешно оформлен!");
                    _context.Add(treaty);
                     await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Данные о договоре не были добавлены. Вызвано исключение!");
            }
            return View(treaty);
        }

        // GET: Treaties/Edit/5
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treaty = await _context.Treatys.FindAsync(id);
            if (treaty == null)
            {
                return NotFound();
            }
            return View(treaty);
        }

        // POST: Treaties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeTreaty,TypeOfInsuranceTreaty,PolicySeries,PolicyNumber,InsurancePremium,InsuranceSum,DateOfPaymentTreaty,TreatyCurrency,StateOfTreaty,IdClient")] Treaty treaty)
        {
            if (id != treaty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Данные о договоре успешно изменены!");
                    _context.Update(treaty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Данные о договоре не были изменены. Вызвано исключение!");
                    if (!TreatyExists(treaty.Id))
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
            return View(treaty);
        }

        // GET: Treaties/Delete/5
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treaty = await _context.Treatys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treaty == null)
            {
                return NotFound();
            }

            return View(treaty);
        }

        // POST: Treaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var treaty = await _context.Treatys.FindAsync(id);
                _context.Treatys.Remove(treaty);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Договор успешно удалён!");
            }
            catch (Exception)
            {
                _logger.LogError("Договор не был удалён. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TreatyExists(int id)
        {
            return _context.Treatys.Any(e => e.Id == id);
        }
    }
}
