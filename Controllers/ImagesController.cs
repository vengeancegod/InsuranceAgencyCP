using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace InsuranceAgency__.Controllers
{
    public class ImagesController : Controller
    {
        private readonly string wwwrootImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
        public ActionResult Index()
        {
            List<string> images = Directory.GetFiles(wwwrootImages, "*.jpg").Select(Path.GetFileName).ToList();
            return View(images);
        }
        [HttpPost]
        public async Task<ActionResult> Index(IFormFile myfile)
        {
            if(myfile != null)
            {
                var path = Path.Combine(wwwrootImages, DateTime.Now.Ticks.ToString() + Path.GetExtension(myfile.FileName));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await myfile.CopyToAsync(stream);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DownloadFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", filePath);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var contentType = "APLLICATION/octet-stream";
                var fileName = Path.GetFileName(path);
                return File(memory, contentType, fileName);
            }
            return View();
        }


        public IActionResult DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", filePath);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}
