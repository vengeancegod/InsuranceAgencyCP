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
    public class PaymentsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(InsuranceAgencyDataContext context, ILogger<PaymentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер оплат успешно запущен!");
            }
            catch
            {
                _logger.LogError("Контроллер оплаты не был запущен. Вызвано исключение!");
            }
            return View(await _context.Payments.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string FIO)
        {
            var payment = _context.Payments.ToList().Where(p => p.FIO.StartsWith(FIO));
            return View(payment);
        }
        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReceiptNumber,FIO,DatePayment")] Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Оплата успешно проведена!");
                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception) 
            {
                _logger.LogError("Оплата не была проведена. Вызвано исключение!");
            }
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReceiptNumber,FIO,DatePayment,ClientId")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Данные об оплате изменены успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Данные об оплате не были изменены. Вызвано исключение!");
                    if (!PaymentExists(payment.Id))
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
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var payment = await _context.Payments.FindAsync(id);
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Данные об оплате успешно удалены !");
            }
            catch (Exception)
            {
                _logger.LogError("Данные об оплате не были удалены. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
