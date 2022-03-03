using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DrinkVendingMachine.Models;

namespace DrinkVendingMachine.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
           //  User = new HashSet<User>();
            
        }

        public ApplicationUser(string username) : base(username)
        {
           
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }

        //public virtual ICollection<User> User { get; set; }

    }
}
