using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkVendingMachine.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly MachineContext _db;
        public HomeController(MachineContext db) { _db = db; }
        public IActionResult Index()
        {
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").ToList();
            //var sum0 = _db.Memory.Where(c=>c.Id==1).ToList();

            return View(sum);
        }
    }
}
