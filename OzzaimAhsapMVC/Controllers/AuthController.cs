using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OzzaimAhsapMVC.Models;
using OzzaimAhsapMVC.Security.Hashing;
using OzzaimAhsapMVC.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using OzzaimAhsap.Models;
using Microsoft.EntityFrameworkCore;


namespace OzzaimAhsap.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly AhsapContext _context;
        public AuthController(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
            _context = new AhsapContext();
        }
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(u => u.Email.Equals(loginModel.Email));
            if (user is null) return NotFound();
            if (!HashingHelper.VerifyPasswordHash(loginModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest();
            }
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Redirect("/Admin");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string firstName, string lastName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var data = await _context.Users.AddAsync(new()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            });
            await _context.SaveChangesAsync();
            var result = _tokenHelper.CreateToken(data.Entity);
            if (result.Token is null) return BadRequest();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}