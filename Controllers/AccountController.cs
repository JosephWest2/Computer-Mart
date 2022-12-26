using Computer_Mart.Data;
using Computer_Mart.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Computer_Mart.Statics;
using Azure.Identity;

namespace Computer_Mart.Controllers
{
    public class AccountController : Controller
    {
		private readonly Computer_MartContext _context;

		public AccountController(Computer_MartContext context)
		{
			_context = context;
		}

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Credential credential)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.Username == credential.Username);

            if (user == null)
            {
                ViewData["Alert"] = "Invalid Username";
                return View();
            }
            else if (!AuthenticateUser.CheckPassword(credential.Password, user.PasswordHash))
            {
                ViewData["Alert"] = "Invalid Password";
                return View();
            }

            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("userId", user.Id.ToString())
                };
            if (user.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            ClaimsIdentity identity = new ClaimsIdentity(claims, Constants.AuthCookieString);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(Constants.AuthCookieString, claimsPrincipal);

            return RedirectToAction("Index", "Computers");
        }

        public async  Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Constants.AuthCookieString);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterCredential credential)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.FirstOrDefault(user => user.Username == credential.Username) != null)
                {
                    ViewData["Alert"] = "Username Taken";
                    return View();
                }
                User newUser = new User()
                {
                    Username = credential.Username,
                    PasswordHash = AuthenticateUser.CreatePasswordHash(credential.Password),
                    Admin = false
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                ViewData["Alert"] = "Successfully Registered";
                return RedirectToAction(nameof(Login));
            }
            
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
