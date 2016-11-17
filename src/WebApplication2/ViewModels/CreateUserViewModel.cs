using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}