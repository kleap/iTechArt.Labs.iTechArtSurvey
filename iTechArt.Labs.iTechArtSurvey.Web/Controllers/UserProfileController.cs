﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using iTechArt.Labs.iTechArtSurvey.BusinessLayer.UserManagement;
using iTechArt.Labs.iTechArtSurvey.Web.ViewModels.UserProfile;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace iTechArt.Labs.iTechArtSurvey.Web.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private SignInManager _signInManager;
        private SurveyUserManager _userManager;

        public UserProfileController()
        {
        }

        public UserProfileController(SurveyUserManager userManager, SignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public SurveyUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<SurveyUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var userProfileModel = new UserProfileViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };

            return View(userProfileModel);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeEmail(string Email)
        {
            var result = await UserManager.ChangeEmailAsync(User.Identity.GetUserId(), Email);
            if (result.Succeeded)
            {
                return PartialView(new UserProfileChangeEmailViewModel { Email = Email });
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChangeName(UserProfileChangeNameViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var storedUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var result = await UserManager.ChangeNameAsync(storedUser.Id, user.Name);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(storedUser, true, true);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(UserProfileChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion
    }
}