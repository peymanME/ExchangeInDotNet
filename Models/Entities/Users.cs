using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Exchange.Models.Entities
{
    public class Users:IdentityUser
    {
       [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsersId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual Members Members { get; set; }

        public string getFullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Users> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


    }
}