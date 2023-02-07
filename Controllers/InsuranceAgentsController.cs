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
    public class InsuranceAgentsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<InsuranceAgentsController> _logger;

        public InsuranceAgentsController(InsuranceAgencyDataContext context, ILogger<InsuranceAgentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: InsuranceAgents
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер страховых агентов успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Контроллер страховых агентов не был запущен. Вызвано исключение!");
            }
            return View(await _context.InsuranceAgents.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string FIO)
        {
            var agent = _context.Clients.ToList().Where(p => p.FIO.StartsWith(FIO));
            return View(agent);
        }
        // GET: InsuranceAgents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceAgent = await _context.InsuranceAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceAgent == null)
            {
                return NotFound();
            }

            return View(insuranceAgent);
        }

        // GET: InsuranceAgents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceAgents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO,BirthDate,Age,Wage")] InsuranceAgent insuranceAgent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Информация о страховом агенте успешно добавлена!");
                    _context.Add(insuranceAgent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Информация о страховом агенте не была добавлена. Вызвано исключение!");
            }
            return View(insuranceAgent);
        }

        // GET: InsuranceAgents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceAgent = await _context.InsuranceAgents.FindAsync(id);
            if (insuranceAgent == null)
            {
                return NotFound();
            }
            return View(insuranceAgent);
        }

        // POST: InsuranceAgents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,BirthDate,Age,Wage")] InsuranceAgent insuranceAgent)
        {
            if (id != insuranceAgent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceAgent);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Данные изменены успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Данные не были изменены. Вызвано исключение!");
                    if (!InsuranceAgentExists(insuranceAgent.Id))
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
            return View(insuranceAgent);
        }

        // GET: InsuranceAgents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceAgent = await _context.InsuranceAgents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceAgent == null)
            {
                return NotFound();
            }

            return View(insuranceAgent);
        }

        // POST: InsuranceAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var insuranceAgent = await _context.InsuranceAgents.FindAsync(id);
                _context.InsuranceAgents.Remove(insuranceAgent);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Данные успешно удалены !");
            }
            catch (Exception)
            {
                _logger.LogError("Данные не были удалены. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceAgentExists(int id)
        {
            return _context.InsuranceAgents.Any(e => e.Id == id);
        }
    }
}
