using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Microsoft.Owin.Security;
using AutenticateWithOutIdentity.Models;


namespace AutenticateWithOutIdentity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public CustomUserManager PCustomUserManager { get; private set; }

        public AccountController() : this(new CustomUserManager())
        {
        }

        public AccountController(CustomUserManager customUserManager)
        {
            PCustomUserManager = customUserManager;
        }

        public ActionResult Index()
        {
            return View();
        }


        // 
        // GET: /Account/Login 
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // 
        // POST: /Account/Login 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await PCustomUserManager.FindUserAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form 
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ApplicationUser pUser)
        {
            var bo_Object = new BO_BusinessManagement.Bo_Object()
            {
                LIdObject = 3011
            };
            var bo_Status = new BO_BusinessManagement.Bo_Status()
            {
                LIdStatus = "APPRO"
            };
            var bo_TypeIdentification = new BO_BusinessManagement.Bo_TypeIdentification()
            {
                LIdTypeIdentification = 2
            };
            var bo_Role = new BO_BusinessManagement.Bo_Role()
            {
                LIdRole = 1
            };
            PasswordHasher lPass = new PasswordHasher();
            pUser.PasswordHash = lPass.HashPassword(pUser.PasswordHash); ;
            var bo_User = new BO_BusinessManagement.Bo_User() {
                LBirthDate = pUser.BirthDate,
                LEmail = "",
                LFLastName = pUser.LastName,
                LException = "",
                LFNameUser = pUser.FirstName,
                LIdUser = 0,
                LInnerException = "",
                LMessageDao = "",
                LNoIdentification = pUser.NoIdentification,
                LObject = bo_Object,
                LPassword = pUser.PasswordHash,
                LSLastName = pUser.SLastName,
                LSNameUser = pUser.SecondName,
                LStatus = bo_Status,
                LTypeIdentification = bo_TypeIdentification,
                LUser = pUser.User,
                LRole = bo_Role
            };

            var lResult = Bll_Business.Bll_User.bll_InsertUser(bo_User);
            if (string.IsNullOrEmpty(lResult))
            {
                return View("Login");
            }
            return View();
        }



        // POST: /Account/LogOff 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing && PCustomUserManager != null)
            {
                PCustomUserManager.Dispose();
                PCustomUserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers 
        // Used for XSRF protection when adding external logins 
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            ///Open Question- Hear it create claimIdentity. But we nothing add as such Claims but just User object. 
            //public virtual Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType); 
            var identity = await PCustomUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            //var identity = await UserManager1.CreateAsync(user);//, DefaultAuthenticationTypes.ApplicationCookie); 

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}