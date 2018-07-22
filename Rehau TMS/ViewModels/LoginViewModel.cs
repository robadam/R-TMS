using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

    }

    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać minimum {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są zgodne.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name= "Rodzaj użytkownika")]
        public string RoleName { get; set; }
        
        [Display(Name= "Status")]
        public bool IsActive { get; set; }

    }

    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }

        [Required]
        [Display(Name = "Rodzaj użytkownika")]
        public string RoleName { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

    }




    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać minimum {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są zgodne.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class UserListViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string FullName
        {
            get { return Name + " " + Surname; }
        }

    }
}