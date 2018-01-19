using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutenticateWithOutIdentity.Models
{
    public class ApplicationRole : IRole
    {
        public DateTime ModificationDate { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public ApplicationRole()
        {
            ModificationDate = DateTime.Now;
        }
    }
}