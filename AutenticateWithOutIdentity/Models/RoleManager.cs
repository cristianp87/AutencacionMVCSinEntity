using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutenticateWithOutIdentity.Models
{
    public class CustomRoleManager : RoleManager<ApplicationRole>
    {
        public CustomRoleManager() : base(new CustomRoleStore())
        {
            //We can retrieve Old System Hash Password and can encypt or decrypt old password using custom approach. 
            //When we want to reuse old system password as it would be difficult for all users to initiate pwd change as per Idnetity Core hashing. 

        }
    }
}