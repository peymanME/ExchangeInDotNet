using System.Threading.Tasks;
using Exchange.Models.Entities;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Exchange.Models;
using Exchange.Models.ViewModels;
using Exchange.Controllers.Base;

namespace Exchange.Controllers
{
    public class IndexController : BaseController
    {

        public ActionResult Index()
        {
            return View("~/Views/Home/index.cshtml");
        }

        public ActionResult Login()
        {
            return PartialView("~/Views/Home/index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users { FirstName=model.FirstName, LastName=model.LastName, UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //user = await UserManager.FindByIdAsync(user.Id );
                    return RedirectToAction("Login", "Index");
                }
                AddErrors(result);
            }
             return PartialView("~/Views/Home/_Register.cshtml");
        }

        public ActionResult Register()
        {
            return PartialView("~/Views/Home/_Register.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Home/Index.cshtml");
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Exchange");    //RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return PartialView("~/Views/Home/Index.cshtml", (AuthViewModel)model);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon();
            return RedirectToAction("Index", "Index");
        }

        public async Task<ActionResult> Menu()
        {
            Users user = await UserManager.FindByNameAsync(User.Identity.Name);
            Session["FullName"] = user.getFullName();
            return PartialView("~/Views/Shared/Menu/_LoginMenu.cshtml");
        }

    }
}