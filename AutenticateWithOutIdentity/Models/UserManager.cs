using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutenticateWithOutIdentity.Models
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {


        //public CustomUserManager(IUserStore<ApplicationUser> store) : base(store)
        //{
        //    this.PasswordHasher = new OldSystemPasswordHasher();
        //}

        public CustomUserManager() : base(new CustomUserSore())
        {
            //We can retrieve Old System Hash Password and can encypt or decrypt old password using custom approach. 
            //When we want to reuse old system password as it would be difficult for all users to initiate pwd change as per Idnetity Core hashing. 
            this.PasswordHasher = new OldSystemPasswordHasher();
        }

        public async System.Threading.Tasks.Task<ApplicationUser> FindUserAsync(string userName, string password)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = await FindByNameAsync(userName);
            if(applicationUser != null)
            {
                PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(applicationUser.Password, password);
                if (result == PasswordVerificationResult.Success)
                {
                    return applicationUser;
                }
                return null;

            }
                

            return applicationUser;
        }
    }

    /// <summary> 
    /// Use Custom approach to verify password 
    /// </summary> 
    public class OldSystemPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        //public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        //{

        //    //Here we will place the code of password hashing that is there in our current solucion.This will take cleartext anad hash 
        //    //Just for demonstration purpose I always return true.     
        //    if (true)
        //    {


        //        return PasswordVerificationResult.SuccessRehashNeeded;
        //    }
        //    else
        //    {
        //        return PasswordVerificationResult.Failed;
        //    }
        //}
    }
}