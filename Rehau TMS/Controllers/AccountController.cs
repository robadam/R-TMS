using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Rehau_TMS.ViewModels;
using Rehau_TMS.Models;

namespace Rehau_TMS.Controllers
{
    public class AccountController : Controller
    {

        ApplicationDbContext _context = new ApplicationDbContext();

        //User menager starts here
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //User menager End



        public ActionResult Index()
        {
            var userslist = (from user in _context.Users where user.Name != "Admin"
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Name = user.Name,
                                      Surname = user.Surname,
                                      IsActive = user.IsActive,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in _context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserListViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Name = p.Name,
                                      Surname = p.Surname,
                                      IsActive = p.IsActive,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(userslist);
        }

        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: Account/Login POST method
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Login, model.Password);

                if (user != null && user.IsActive)
                {
                    await SignInAsync(user);
                    return RedirectToLocal(returnUrl);
                }
                else if (user != null && !user.IsActive)
                {
                    ModelState.AddModelError("", "Użytkownik jest zablokowany.");
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło.");
                }
                

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            var allRoles = (new ApplicationDbContext()).Roles.Where(r => r.Name != "Admin").OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            ViewBag.Roles = allRoles;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Login, Name = model.Name, Surname = model.Surname, IsActive = true};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    UserManager.AddToRole(user.Id, model.RoleName);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Edit(string Id)
        {
            ApplicationUser appUser = new ApplicationUser();
            appUser = UserManager.FindById(Id);
            string userRoles = UserManager.GetRoles(appUser.Id).FirstOrDefault();
            EditUserViewModel user = new EditUserViewModel();
            user.UserId = appUser.Id;
            user.Login = appUser.UserName;
            user.Name = appUser.Name;
            user.Surname = appUser.Surname;
            user.IsActive = appUser.IsActive;
            user.RoleName = userRoles;

            var allRoles = (new ApplicationDbContext()).Roles.Where(r => r.Name != "Admin").OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = allRoles;

            return View(user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = manager.FindByName(model.Login);
            var oldRoleId = currentUser.Roles.SingleOrDefault().RoleId;
            var oldRoleName = _context.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;
            currentUser.Name = model.Name;
            currentUser.Surname = model.Surname;
            currentUser.IsActive = model.IsActive;
            await manager.UpdateAsync(currentUser);

            if (oldRoleName != model.RoleName)
            {
                manager.RemoveFromRole(currentUser.Id, oldRoleName);
                manager.AddToRole(currentUser.Id, model.RoleName);
            }

            var ctx = store.Context;
            ctx.SaveChanges();
            return RedirectToAction("Index", "Account");
        }


        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }



        //Sign in async declaration
        private async Task SignInAsync(ApplicationUser user)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, await user.GenerateUserIdentityAsync(UserManager));
        }


        //Redirect declaration
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Schedule");
            }
        }

        //Error stack
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }

}