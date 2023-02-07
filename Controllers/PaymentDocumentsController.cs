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
    public class PaymentDocumentsController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly ILogger<PaymentDocumentsController> _logger;

        public PaymentDocumentsController(InsuranceAgencyDataContext context, ILogger<PaymentDocumentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: PaymentDocuments
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Контроллер платежных документов успешно запущен!");
            }
            catch
            {
                _logger.LogError("Контроллер платежных документов не был запущен. Вызвано исключение!");
            }
            return View(await _context.PaymentDocuments.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(int AccountNumber)
        {
            var paydoc = _context.PaymentDocuments.ToList().Where(p => p.AccountNumber == AccountNumber);
            return View(paydoc);
        }
        // GET: PaymentDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDocument = await _context.PaymentDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentDocument == null)
            {
                return NotFound();
            }

            return View(paymentDocument);
        }

        // GET: PaymentDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNumber,BeneficiaryBankName,InvoicingDate,NameOfService,ServiceAmount,FIOPolicyholder,PaymentMethod,ClientId")] PaymentDocument paymentDocument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Документы успешно добавлены!");
                    _context.Add(paymentDocument);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                _logger.LogError("Документы не были добавлены. Вызвано исключение!");
            }
            return View(paymentDocument);
        }

        // GET: PaymentDocuments/Edit/5
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDocument = await _context.PaymentDocuments.FindAsync(id);
            if (paymentDocument == null)
            {
                return NotFound();
            }
            return View(paymentDocument);
        }

        // POST: PaymentDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,BeneficiaryBankName,InvoicingDate,NameOfService,ServiceAmount,FIOPolicyholder,PaymentMethod")] PaymentDocument paymentDocument)
        {
            if (id != paymentDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentDocument);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Доументы изменены успешно!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation("Документы не были изменены. Вызвано исключение!");
                    if (!PaymentDocumentExists(paymentDocument.Id))
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
            return View(paymentDocument);
        }
        [Authorize(Roles = "Страховой агент")]
        // GET: PaymentDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDocument = await _context.PaymentDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentDocument == null)
            {
                return NotFound();
            }

            return View(paymentDocument);
        }

        // POST: PaymentDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var paymentDocument = await _context.PaymentDocuments.FindAsync(id);
                _context.PaymentDocuments.Remove(paymentDocument);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Документы успешно удалены !");
            }
            catch (Exception)
            {
                _logger.LogError("Документы не были удалены. Вызвано исключение!");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentDocumentExists(int id)
        {
            return _context.PaymentDocuments.Any(e => e.Id == id);
        }
    }
}
