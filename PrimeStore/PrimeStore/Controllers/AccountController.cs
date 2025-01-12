using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Models;
using PrimeStore.ViewModels;

namespace PrimeStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAllFolder _allFolder;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IAllFolder allFolder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _allFolder = allFolder;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User 
                { 
                    Login = model.Login,
                    Username = model.Username,
                    Password = model.Password,
                    UserName = model.Username
                };
                
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    string check = user.Id;
                    _allFolder.Folder = new Folder
                    {
                        Foldername = "root",
                        CreationTime = DateTime.Now,
                        UserId = user.Id,
                        InBasket = false
                    };
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl ?? Url.Content("~/") } );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
