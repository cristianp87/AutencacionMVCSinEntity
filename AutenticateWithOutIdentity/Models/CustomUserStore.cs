using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Bll_Business;
using BO_BusinessManagement;
using System.Threading.Tasks;

namespace AutenticateWithOutIdentity.Models
{
    public class CustomUserSore : IUserStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
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
            Bo_User lUser = new Bo_User();
            var lIdUser = 0;
            if (int.TryParse(userId, out lIdUser))
            { 
                lUser = Bll_User.bll_GetUserById(Convert.ToInt32(userId));
                if (string.IsNullOrEmpty(lUser.LException))
                {

                    var lUserapp = new ApplicationUser
                    {
                        Id = lUser.LIdUser.ToString(),
                        BirthDate = DateTime.Now,
                        CreateDate = DateTime.Now,
                        PasswordHash = lUser.LPassword,
                        User = lUser.LUser,
                        UserName = lUser.LFNameUser + " " + lUser.LFLastName

                    };

                    return System.Threading.Tasks.Task.Run(() => { return lUserapp; }); //System.Threading.Tasks.Task.FromResult(lUserapp);
                }
                else
                {
                    return null;
                }
            }
            else { return null; }
        }


        public System.Threading.Tasks.Task<ApplicationUser>  FindByNameAsync(string userName)
        {
            Bo_User lUser = new Bo_User();
            lUser = Bll_User.bll_GetUserByUser(userName);
            if (string.IsNullOrEmpty(lUser.LException))
            {

                var lUserapp = new ApplicationUser
                {
                    Id = lUser.LIdUser.ToString(),
                    BirthDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    PasswordHash = lUser.LPassword,
                    User = lUser.LUser,
                    UserName = lUser.LFNameUser + " " + lUser.LFLastName                    
           
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

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(string userId)
        {
            IList<Bo_Role> lRole;
            lRole = Bll_Role.GetRolesByUser(Convert.ToInt32(userId));
            IList<string> lListApprole= null;
            if (string.IsNullOrEmpty(lRole.First().LException))
            {
                lRole.ToList().ForEach(item =>
                {
                    lListApprole.ToList().Add(item.LNameRole);
                });

                return Task.Run(() => { return lListApprole; }); //System.Threading.Tasks.Task.FromResult(lUserapp);
            }
            else
            {
                return null;
            }
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            IList<Bo_Role> lRole;
            lRole = Bll_Role.GetRolesByUser(Convert.ToInt32(user.Id));
            IList<string> lListApprole = new List<string>();
            
            if (string.IsNullOrEmpty(lRole.First().LException))
            {
                lRole.ToList().ForEach(item =>
                {
                    lListApprole.Add(item.LNameRole);
                });

                return Task.Run(() => { return lListApprole; }); //System.Threading.Tasks.Task.FromResult(lUserapp);
            }
            else
            {
                return null;
            }
        }
    }
}