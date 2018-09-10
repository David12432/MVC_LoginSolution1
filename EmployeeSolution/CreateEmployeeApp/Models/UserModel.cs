using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CreateEmployeeApp.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        public String userName { get; set; }

        [Required]
        [PasswordPropertyText]
        //[StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public String password { get; set; }

        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public String confirmPassword { get; set; }



    }
}