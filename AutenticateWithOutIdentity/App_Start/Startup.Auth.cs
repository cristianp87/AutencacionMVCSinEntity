using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using Microsoft.AspNet.Identity.Owin;
using AutenticateWithOutIdentity.Models;

namespace AutenticateWithOutIdentity
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864 
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user 
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<CustomUserManager, ApplicationUser>(
                    validateInterval: TimeSpan.FromMinutes(1),
                    regenerateIdentity: (manager, user) =>  user.GenerateUserIdentityAsync(manager)),
                },
                SlidingExpiration = true,
                //Use this to customize the timeout duration if the default is too short/long
                ExpireTimeSpan = TimeSpan.FromMinutes(1)
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider 
            // app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie); 

            // Uncomment the following lines to enable logging in with third party login providers 
            //app.UseMicrosoftAccountAuthentication( 
            //    clientId: "", 
            //    clientSecret: ""); 

            //app.UseTwitterAuthentication( 
            //   consumerKey: "", 
            //   consumerSecret: ""); 

            //app.UseFacebookAuthentication( 
            //   appId: "", 
            //   appSecret: ""); 

            //app.UseGoogleAuthentication(); 
        }
    }
}