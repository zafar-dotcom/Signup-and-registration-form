using Microsoft.Build.Framework;
using Xunit;
using System.ComponentModel.DataAnnotations;

using Xunit.Abstractions;
using Xunit.Sdk;


namespace Complete.Models
{
    public class User_signup_model
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full_Name")]
        public string Fullname { get; set; }
       // [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email is required")]
         
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        [EmailAddress(ErrorMessage="invalid email formate")]
        public string Email { get; set; }
       // [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "{0} must contain at least {2} and max {1} character", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase      Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }
        //[Display(Name ="Please Enter Confirm Password")]
        [Compare("Password" ,ErrorMessage="Password and confirm password must be same")]
        public string Confirmpassword { get; set; }
    }
}
