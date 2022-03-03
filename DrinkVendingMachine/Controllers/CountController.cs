using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkVendingMachine.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.Controllers
{
    public class CountController : Controller
    {

        private readonly MachineContext _db;
        public CountController(MachineContext db) 
        {
            _db = db;
        }
        public List<Memory> Memories = null;

        public IActionResult Index()
        {
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").ToList();
            return View(sum);
        }

        //[HttpPost]
        //public IActionResult GG(Products obj) 
        //{
        //    return View(obj); 
        //}

        [HttpPost]
        public dynamic One() {
            int one = 1;
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").FirstOrDefault();
            sum.One = sum.One + one;
            sum.Sum = sum.One + sum.Two + sum.Five + sum.Ten;
            _db.Memory.Update(sum);
            _db.SaveChanges();
          //  return RedirectToAction("");
            return Redirect("/Product/All"); //Надо придумать как вернуться на стартовую странтцу
        }
        [HttpPost]
        public dynamic Two()
        {
            int two = 2;
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").FirstOrDefault();
         
            sum.Two = sum.Two + two;
            sum.Sum = sum.One + sum.Two + sum.Five + sum.Ten;
            _db.Memory.Update(sum);
            _db.SaveChanges();
            Redirect("/Product/All");
            return sum.Sum; //Надо придумать как вернуться на стартовую странтцу

        }
        [HttpPost]
        public dynamic Five()
        {
            int five = 5;
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").FirstOrDefault();
            
            sum.Five = sum.Five + five;
            sum.Sum = sum.One + sum.Two + sum.Five + sum.Ten;
            _db.Memory.Update(sum);
            _db.SaveChanges();
            
            return Redirect("localhost:44394"); //Надо придумать как вернуться на стартовую странтцу

        }
        [HttpPost]
        public dynamic Ten()
        {
            int ten = 10;
            var sum = _db.Memory.FromSqlRaw($"SELECT * FROM Memory").FirstOrDefault();
            sum.Ten = sum.Ten + ten;
            sum.Sum = sum.One + sum.Two + sum.Five + sum.Ten;
            _db.Memory.Update(sum);
            _db.SaveChanges();

            return Redirect("localhost:44394"); //Надо придумать как вернуться на стартовую странтцу

        }



        [HttpPost]
        public dynamic One0()
        {
            int one = 1;
            var sum = _db.Memory.Where(c => c.Id == 1).FirstOrDefault();
            var buy = _db.Products.Where(c => c.Price == 1).FirstOrDefault();

            if (sum.Sum >=  one)
            {
                sum.Sum = sum.Sum - one;
            }
            if (sum.Sum < one)
            {
                return "недостаточно средств";
            }
            if (buy.Quantity > 0)
            {
                buy.Quantity = buy.Quantity - 1;
                _db.Products.Update(buy);
                _db.SaveChanges();
            }
            else 
            {
                return "нет товара";
            }
            _db.Memory.Update(sum);
            _db.SaveChanges();
            //  return RedirectToAction("");
            return Redirect("/Product/All"); //Надо придумать как вернуться на стартовую странтцу
        }
        [HttpPost]
        public dynamic Two0()
        {
            int two = 2;
            var sum = _db.Memory.Where(c => c.Id == 1).FirstOrDefault();
            var buy = _db.Products.Where(c => c.Price == 2).FirstOrDefault();

            if (sum.Two >= two)
            {
                sum.Two = sum.Two - two;
            }
            if (sum.Two < two)
            {
                return "недостаточно средств";
            }
            if (buy.Quantity > 0)
            {
                buy.Quantity = buy.Quantity - 1;
                _db.Products.Update(buy);
                _db.SaveChanges();
            }
            else
            {
                return "нет товара";
            }

            _db.Memory.Update(sum);
            _db.SaveChanges();
            return Redirect("/Product/All");
           

        }
        [HttpPost]
        public dynamic Five0()
        {
            int five = 5;
            var sum = _db.Memory.Where(c => c.Id == 1).FirstOrDefault();
            var buy = _db.Products.Where(c => c.Price == 5).FirstOrDefault();

            if (sum.Five >= five)
            {
                sum.Five = sum.Five - five;
            }
            if (sum.Five < five)
            {
                return "недостаточно средств";
            }
            if (buy.Quantity > 0)
            {
                buy.Quantity = buy.Quantity - 1;
                _db.Products.Update(buy);
                _db.SaveChanges();
            }
            else
            {
                return "нет товара";
            }
            _db.Memory.Update(sum);
            _db.SaveChanges();

            return Redirect("/Product/All"); //Надо придумать как вернуться на стартовую странтцу

        }
        [HttpPost]
        public dynamic Ten0()
        {
            int ten = 10;
            var sum = _db.Memory.Where(c => c.Id == 1).FirstOrDefault();
            var buy = _db.Products.Where(c => c.Price == 10).FirstOrDefault();

            if (sum.Ten >=  ten)
            {
                sum.Ten = sum.Ten - ten;
            }
            if (sum.Ten < ten)
            {
                return "недостаточно средств";
            }
            if (buy.Quantity > 0)
            {
                buy.Quantity = buy.Quantity - 1;
                _db.Products.Update(buy);
                _db.SaveChanges();
            }
            else
            {
                return "нет товара";
            }
            _db.Memory.Update(sum);
            _db.SaveChanges();

            return Redirect("/Product/All"); //Надо придумать как вернуться на стартовую странтцу
         
        }

        [HttpPost]
        public dynamic payment()
        {
            Dictionary<string, dynamic> payment = new Dictionary<string, dynamic>();
           
            var sum = _db.Memory.Where(c=>c.Id==1).FirstOrDefault();
            var sum0 = _db.Memory.Where(c => c.Id == 2).FirstOrDefault();

            
            var OneCount = sum.One / 1;
            var TwoCount = sum.Two / 2;
            var FiveCount = sum.Five / 5;
            var TenCount = sum.Ten / 10;


          
            payment.Add("Сдача наминалом 1 ", OneCount);
            payment.Add("Сдача наминалом 2 ", TwoCount);
            payment.Add("Сдача наминалом 5 ", FiveCount);
            payment.Add("Сдача наминалом 10 ", TenCount);

            sum.One = +0;
            sum.Two = +0;
            sum.Five = +0;
            sum.Ten = +0;

            sum.Sum = sum.One + sum.Two + sum.Five + sum.Ten;//Если деньги отданы то память автомата освобождается 
            _db.Memory.Update(sum);
            _db.SaveChanges();

            return payment;
        }
            
    }
}
