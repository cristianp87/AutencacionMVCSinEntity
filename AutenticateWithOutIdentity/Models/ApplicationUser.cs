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
        public DateTime ModificationDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string LastName { get; set; }
        public string SLastName { get; set; }
        public int IdTypeIdentification { get; set; }

        public string NoIdentification { get; set; }

        public int IdObject { get; set; }

        public string User { get; set; }

        public string UserName { get; set; }

        public string IdStatus { get; set; }

        public IList<string> Roles { get; set; }

        public ApplicationUser()
        {
            ModificationDate = DateTime.Now;
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