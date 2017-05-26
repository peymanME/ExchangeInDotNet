using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exchange.Models.ViewModels
{
    public partial class AuthViewModel
    {

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public virtual string RetypePassword { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public static explicit operator AuthViewModel (LoginViewModel v)
        {
            AuthViewModel authViewModel = new AuthViewModel();
            authViewModel.Email = v.Email;
            authViewModel.Password = v.Password;
            return authViewModel;

        }
    }
}