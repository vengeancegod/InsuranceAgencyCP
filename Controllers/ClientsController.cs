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
    public class ClientsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(InsuranceAgencyDataContext context, ILogger<ClientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Clients
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер клиентов успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Контроллер клиентов не был запущен. Вызвано исключение!");
            }
            return View(await _context.Clients.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string FIO)
        {
            var client = _context.Clients.ToList().Where(p => p.FIO.StartsWith(FIO));
            return View(client);
        }
        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeClient,FIO,PassportSeries,PassportNumber,BirthDate,PlaceOfResidence")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Клиент успешно добавлен!");
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Клиент не был добавлен. Вызвано исключение!");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeClient,FIO,PassportSeries,PassportNumber,BirthDate,PlaceOfResidence")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Данные клиента изменены успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Данные клиента не были изменены. Вызвано исключение!");
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Данные клиента успешно удалены!");
            }
            catch (Exception)
            {
                _logger.LogError("Данные клиента не были удалены. Вызвано исключение!");
            }
        
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
