using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DrinkVendingMachine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using DrinkVendingMachine.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace DrinkVendingMachine.Controllers
{
    public class AdminController : Controller
    {
        private readonly MachineContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            MachineContext db)
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        private List<User> people = new List<User>
        {
            new User {Email="admin@gmail.com", PasswordHash="12345" },
            new User { Email="qwerty@gmail.com", PasswordHash="55555" }
        };
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "Administrator")]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Policy = "Manager")]
        public IActionResult Manager()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Admin", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("RegisterExternal", new ExternalLoginViewsModel
            {
                ReturnUrl = returnUrl,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Name)
            });
        }

        [AllowAnonymous]
        public IActionResult RegisterExternal(ExternalLoginViewsModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("RegisterExternal")]
        public async Task<IActionResult> RegisterExternalConfirmed(ExternalLoginViewsModel model)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new ApplicationUser(model.UserName);

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var claimsResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
                if (claimsResult.Succeeded)
                {
                    var identityResult = await _userManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewsModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }

        public static class Databaseintializer
        {
            public static void Init(IServiceProvider serviceProvider)
            {
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser
                {
                    UserName = "User",
                    LastName = "LastName",
                    FirstName = "FirstName"
                };


                var result = userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
                }
                //_db.Users.Add(user);
                //_db.SaveChanges();
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(LoginViewsModel model)
        //{

        //    var result1 = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
        //    int a = 0;

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }


        //    var user1 = await _db.User.
        //                          Where(x => x.Name == model.UserName &&
        //                          x.Password == model.Password)
        //                          .FirstOrDefaultAsync();




        //    string Name = user1.Name.ToString();
        //    if (user1 == null)
        //    {
        //        ModelState.AddModelError("", "Check the correctness of the entered data");
        //        return View(model);
        //    }

        //    var user2 = new IdentityUser { UserName = model.UserName, PasswordHash = model.Password }; //добавление Email и Phone
        //    var user122 = await _userManager.FindByNameAsync(user2.UserName);
        //    /// AQAAAAEAACcQAAAAENehfhkFlpaFt6plXDL38zqlo7UHapNwKQRavdDP7Bp6heA2FMdRvJG7HxuIk4s / 9A ==
        //       var user12 = await _userManager.FindByNameAsync(model.UserName);
        //   // / AQAAAAEAACcQAAAAENehfhkFlpaFt6plXDL38zqlo7UHapNwKQRavdDP7Bp6heA2FMdRvJG7HxuIk4s / 9A ==



        //       "AQAAAAEAACcQAAAAEChAsPZuVu6WLLpKoR2CAnoojxm2vjeKophTzCttuo2tonKvBTaqRTN0rUR1G4ks3w=="
        //    "AQAAAAEAACcQAAAAEMKCatmufiVYRV+/9sOqUCZZE7ZZwa7MyvdbDJ/ksuECXc6SVZhp9TLrarJrBf8hzQ=="
        //    "AQAAAAEAACcQAAAAELXSW8OHuhgYWrxwxrl0KTvCLBjZ3zVUxAuWKpQvSXE+r+rDYGkLqE+Spl+p6IXiqw=="

        //    var user = await _db.Users
        //        .SingleOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);

        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Check the correctness of the entered data");
        //        return View(model);
        //    }


        //   // Создание переменых для лучшей шифровки md5

        //    /* string password = model.Password;
        //     string name = model.UserName;
        //      Когентинация имени пользователя и пароля и помещение в переменую которая используется для шифорования
        //     string NameAndPassword = name + password;

        //     MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //     byte[] encrypt;
        //     UTF8Encoding encode = new UTF8Encoding();
        //      Зашифровать заданную строку пароля в зашифрованные данные
        //     encrypt = md5.ComputeHash(encode.GetBytes(NameAndPassword));
        //     StringBuilder encryptdata = new StringBuilder();
        //      Создаем новую строку, используя зашифрованные данные 
        //     for (int i = 0; i < encrypt.Length; i++)
        //     {
        //         encryptdata.Append(encrypt[i].ToString());
        //     }
        //     var user = await _db.User
        //                   .SingleOrDefaultAsync(x => x.Name == model.UserName && x.Password == encryptdata.ToString());

        //     if (user == null)
        //     {
        //         ModelState.AddModelError("UserName", "Check the correctness of the entered data");
        //         return View(model);
        //     }
        //    */

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name,model.UserName),
        //        new Claim(ClaimTypes.Role, "Administrator"),
        //        new Claim(ClaimTypes.Role, "Manager")
        //    };
        //    var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
        //    var claimsPrincipical = new ClaimsPrincipal(claimsIdentity);
        //    HttpContext.SignInAsync("Cookie", claimsPrincipical);

        //    var ewewwe = _signInManager. ;
        //    var result = await _signInManager.PasswordSignInAsync(user122, model.Password, false, false);
        //    if (result.Succeeded)
        //    {
        //        return Redirect(model.ReturnUrl);
        //    }
        //    return View(model);
        //}
        ///////////////////---------------------------

        //public static void Init(IServiceProvider serviceProvider)
        //{
        //    var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

        //    var user = new ApplicationUser
        //    {
        //        UserName = "User",
        //        LastName = "LastName",
        //        FirstName = "FirstName"
        //    };



        //    var result = userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();

        //    if (result.Succeeded)
        //    {
        //        userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
        //    }
        //    _db.Users.Add(user);
        //    _db.SaveChanges();

        //    ----------------------------------------
        public class LoginViewsModel
        {

            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string ReturnUrl { get; set; }

            public IEnumerable<AuthenticationScheme> ExternalProviders { get; set; }

        }
        public class ExternalLoginViewsModel
        {

            [Required]
            public string UserName { get; set; }
            [Required]
            public string ReturnUrl { get; set; }

            public IEnumerable<AuthenticationScheme> ExternalProviders { get; set; }

        }
    }
}
