using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using Microsoft.AspNetCore.Identity;
using Bl;
namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) { 
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }


        public IActionResult Register()
        {
            return View(new UserModel());
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            if(!ModelState.IsValid)
                return View("Register",model);
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    if(loginResult.Succeeded)
                    {
                        var Myuser = await _userManager.FindByEmailAsync(user.Email);
                        await _userManager.AddToRoleAsync(Myuser, "Customer");
                        Redirect("/Home/index");
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {

            }

            return View(new UserModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model)
        {
            //if (!ModelState.IsValid)
            //    return View("Login", model);
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
            };
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect("~/");
                    else
                        return Redirect(model.ReturnUrl);
                }
            }
            catch (Exception ex)
            {

            }

            return View(new UserModel());
        }

        public IActionResult Login(string returnUrl)
        {
            UserModel model = new UserModel()
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }

    }
}
