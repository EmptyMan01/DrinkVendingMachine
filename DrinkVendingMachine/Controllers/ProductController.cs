using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkVendingMachine.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.Controllers
{
    public class ProductController : Controller
    {
        private readonly MachineContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(MachineContext db, IWebHostEnvironment hostEnvironment) 
        { 
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index(string   rr)
        {

            string r = "sdsd";
            if (r==rr)
            {
                
                return View(await _db.Products.ToListAsync());
            }
            //var sum = _db.Products.FromSqlRaw($"SELECT * FROM Products").ToList();
            //return View(sum[0]);
           
            return Redirect("error.html");
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var image = _db.Products.FromSqlRaw($"SELECT * FROM Products")
                .Include(c=>c.Me)
                .ToList();
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").FirstOrDefault();

            if (image == null)
            {
               return NoContent();
            }
            image[0].Me.Add(sum);
            return View(image);
           
        }

        [HttpGet]
        public IActionResult Create(string rr)
        {
            
            string r = "sdsd";
            if (r == rr)
            {
                return View();
            }
            return Redirect("/error.html");
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(" Quantity, SpriteFile, Sprite, IdProduct")] Products products)
        {
            if (ModelState.IsValid)
            {
                //save sprite wwwroot/sprite
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string spriteFile = Path.GetFileNameWithoutExtension(products.SpriteFile.FileName);
                string extension = Path.GetExtension(products.SpriteFile.FileName);
                products.NameSprite = spriteFile = spriteFile + DateTime.Now.ToString("yymmssfff")+ extension;
                string path = Path.Combine(wwwRootPath + "/Sprite", spriteFile);
                using (var SpriteFile = new FileStream(path, FileMode.Create)) 
                {
                    await products.SpriteFile.CopyToAsync(SpriteFile);
                }

                _db.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id==null)
            {
                return NoContent();

            }
            var image = await _db.Products.FirstOrDefaultAsync(m => m.IdProduct == id);
            if (image ==null)
            {
                return NoContent();
            }
            return View(image);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

           var image = await _db.Products.FindAsync(id);

            //delete image from wwwroot/Sprite
            var spritePath = Path.Combine(_hostEnvironment.WebRootPath, "sprite", image.NameSprite);
            if (System.IO.File.Exists(spritePath))
            {
                System.IO.File.Delete(spritePath);
            }
            //delete the record
            _db.Products.Remove(image);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
