using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using InsuranceAgency__.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceAgency__.Controllers
{
    public class ExpertisesController : Controller
    {
        private readonly InsuranceAgencyDataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string wwwrootD = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Expertises");
        

        public ExpertisesController(InsuranceAgencyDataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Expertises
        public async Task<IActionResult> Index()
        {
            List<string> images = Directory.GetFiles(wwwrootD, "*.png").Select(Path.GetFileName).ToList();
            return View(images);
        }

        // GET: Expertises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertise = await _context.Expertises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expertise == null)
            {
                return NotFound();
            }

            return View(expertise);
        }

        // GET: Expertises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expertises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FIO,PassportNumber,PassportSeries,InsuranceType,Image")] Expertise expertise)
        {
            if (ModelState.IsValid)
            {
                var EntityModel = _context.Add(expertise);
                await _context.SaveChangesAsync();

                int Id = EntityModel.Entity.Id;

                if(Id>0&&expertise.Image!=null)
                {
                    string wwwrootpath = _webHostEnvironment.WebRootPath;
                    string SubDirpath = $"Expertise{Id}";
                    DirectoryInfo directoryInfo = new DirectoryInfo(wwwrootpath + "/Expertises");
                    if (directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                    }
                    directoryInfo.CreateSubdirectory(SubDirpath);

                    string filename = Path.GetFileNameWithoutExtension(expertise.Image.ImageFile.FileName);
                    string extension = Path.GetExtension(expertise.Image.ImageFile.FileName);
                    expertise.Image.ImageTitle = filename + Id + extension;
                    string path = Path.Combine(wwwrootpath + "/Expertises/" + SubDirpath + "/" + expertise.Image.ImageTitle);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                        await expertise.Image.ImageFile.CopyToAsync(FileStream);
                    }
                    _context.Expertises.Update(expertise);
                    await _context.SaveChangesAsync();

                }

                return RedirectToAction(nameof(Index));
            }
            return View(expertise);
        }

        // GET: Expertises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertise = await _context.Expertises.FindAsync(id);
            if (expertise == null)
            {
                return NotFound();
            }
            return View(expertise);
        }

        // POST: Expertises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Страховой агент")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,PassportNumber,PassportSeries,InsuranceType, Image")] Expertise expertise)
        {
            if (id != expertise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expertise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertiseExists(expertise.Id))
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
            return View(expertise);
        }

        // GET: Expertises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertise = await _context.Expertises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expertise == null)
            {
                return NotFound();
            }

            return View(expertise);
        }

        // POST: Expertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expertise = await _context.Expertises.Include(o=>o.Image).FirstOrDefaultAsync(o=>o.Id==id);

            string wwrootpath = _webHostEnvironment.WebRootPath;

            DirectoryInfo df = new DirectoryInfo(wwrootpath+"/Expertises/"+$"Expertise{id}");
            df.Delete(true);

            _context.Expertises.Remove(expertise);

            _context.ImageModels.Remove(expertise.Image);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpertiseExists(int id)
        {
            return _context.Expertises.Any(e => e.Id == id);
        }
    }
}
