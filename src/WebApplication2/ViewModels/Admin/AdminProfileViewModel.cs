using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AdminProfileViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "First Name is required!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone Number is required!")]
        [Phone(ErrorMessage = "Has to be a valid phone number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Has to be a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Address is required!")]
        [EmailAddress(ErrorMessage = "Has to be a valid Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Has to be a valid Email Address")]
        [Display(Name = "Email Address")]
        public string UserName { get; set; }
        [Display(Name = "Super Admin?")]
        public bool SuperAdmin { get; set; }
    }
}