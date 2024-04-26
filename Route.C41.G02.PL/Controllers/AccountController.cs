using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using Route.C41.G02.PL.ViewModels.Account;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
    public class AccountController : Controller
    {
        #region SignUp - Register

        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser>signInManager)
        {
            _userManager = userManager;
			_signInManager= signInManager;

		}
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
       [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userEmail is null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        IsAgree = model.IsAgree,
                    };

                    var result = await _userManager.CreateAsync(user, model.Passoword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                    }
                };
                ModelState.AddModelError(string.Empty, "This user Is Already Exist");
            }
            return View(model);
        }
		[HttpPost]

		public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Passoword);
                    if(flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Passoword, model.RememberMe, false);
                        if(result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index),"Home");
                        
                        if(result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account is Locked");
						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your Account is not confirmed yet!!");

					}
				}
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(model);
        }



        #endregion
    }
}