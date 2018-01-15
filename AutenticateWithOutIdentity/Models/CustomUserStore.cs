using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Bll_Business;
using BO_BusinessManagement;

namespace AutenticateWithOutIdentity.Models
{
    public class CustomUserSore : IUserStore<ApplicationUser>
    {

        public System.Threading.Tasks.Task CreateAsync(ApplicationUser user)
        {
            //Create /Register New User 
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(ApplicationUser user)
        {
            //Delete User 
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<ApplicationUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<ApplicationUser>  FindByNameAsync(string userName)
        {
            Bo_User lUser = new Bo_User();
            lUser = Bll_User.bll_GetUserByUser(userName);
            if (string.IsNullOrEmpty(lUser.LException))
            {

                var lUserapp = new ApplicationUser
                {
                    Id =  lUser.LIdUser.ToString(),
                    BirthDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Password = lUser.LPassword,
                    UserName = lUser.LUser
                };

                return System.Threading.Tasks.Task.Run(()=> { return lUserapp; }); //System.Threading.Tasks.Task.FromResult(lUserapp);
            }
            else
            {
                return null;
            }
        }

        public System.Threading.Tasks.Task UpdateAsync(ApplicationUser user)
        {
            //Update User Profile 
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            // throw new NotImplementedException(); 

        }
    }
}