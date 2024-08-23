using Kyrsova.Data;
using Kyrsova.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Kyrsova.Controllers
{
    public class HomeController : Controller
    {

        private readonly WebappDbContext _context;

        public HomeController(WebappDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        public IActionResult Registrate()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registrate(User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким email уже существует.");
                    return View(model);
                }

                var hashedPassword = HashPassword(model.Password);

                var user = new User
                {
                    Email = model.Email,
                    Password = hashedPassword,
                    FirstName = model.FirstName, 
                    LastName = model.LastName, 
                    PhoneNumber = model.PhoneNumber
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, user.FirstName),
                     new Claim(ClaimTypes.HomePhone, user.PhoneNumber),
                     new Claim(ClaimTypes.Surname, user.LastName),
                     new Claim(ClaimTypes.Email, user.Email)

                };

                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "User");
            }
            return View(model);
        }

        private static string HashPassword(string password)
        {
            var salt = GenerateSalt();
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is not valid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                ViewBag.ErrorMessage = "Некорректные данные.";
                return View(model);
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователь не найден.";
                return View(model);
            }

            if (VerifyPassword(model.Password, user.Password))
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, user.FirstName),
                     new Claim(ClaimTypes.HomePhone, user.PhoneNumber),
                     new Claim(ClaimTypes.Surname, user.LastName),
                     new Claim(ClaimTypes.Email, user.Email)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "User");
            }

            ViewBag.ErrorMessage = "Неверный пароль.";
            return View(model);
        }

        private static bool VerifyPassword(string inputPassword, string storedPassword)
        {
            var parts = storedPassword.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var hashInputPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: inputPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
            return storedHash == hashInputPassword;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}