using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;

namespace AutenticateWithOutIdentity.Models
{
    public class ApplicationUser : IUser
    {
        public DateTime CreateDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }

        public ApplicationUser()
        {
            CreateDate = DateTime.Now;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
    UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one 
            // defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity =
                await manager.CreateIdentityAsync(this,
                    DefaultAuthenticationTypes.ApplicationCookie);
            //userIdentity = null;
            // Add custom user claims here
          //  var userIdentity = IAuthenticationManager;
            return userIdentity;
        }
    }
}