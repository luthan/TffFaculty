using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class FacultyProfileApiModel
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [Display(Name = "Email Address / Username")]
        public string UserName { get; set; }

        public string Prefix { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name (as it appears on ID)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name (as it appears on ID)")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Degree is Required")]
        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Affiliation is Required")]
        [Display(Name = "Affiliation")]
        public string Affiliation { get; set; }

        [Required(ErrorMessage = "Specialty is Required")]
        [Display(Name = "Specialty")]
        public string Specialty { get; set; }

        [Required(ErrorMessage = "Bussiness Phone is Required")]
        [Phone(ErrorMessage = "Invalid formatting.")]
        [Display(Name = "Business Phone")]
        public string BusinessPhone { get; set; }

        [Phone(ErrorMessage = "Invalid formatting.")]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Phone(ErrorMessage = "Invalid formatting.")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Assistant's Name")]
        public string AssistanName { get; set; }

        [Phone(ErrorMessage = "Invalid formatting.")]
        [Display(Name = "Assistant's Phone")]
        public string AssistantPhone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid formatting.")]
        [Display(Name = "Assistant's Email")]
        public string AssistantEmail { get; set; }

        [Required(ErrorMessage = "Street Address is Required")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "State / Province is Required")]
        [Display(Name = "State / Province")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip / Postal Code is Required")]
        [Display(Name = "Zip / Postal Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Official Name is Required")]
        [Display(Name = "Name, as it appears on your official identification")]
        public string OfficialName { get; set; }

        [Required(ErrorMessage = "Date of Birth isRequired")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "1st Hotel Chain")]
        public string Hotel1Name { get; set; }

        [Display(Name = "Hotel Reward #")]
        public string Hotel1MemberNumber { get; set; }

        [Display(Name = "2nd Hotel Chain")]
        public string Hotel2Name { get; set; }

        [Display(Name = "Hotel Reward #")]
        public string Hotel2MemberNumber { get; set; }

        [Display(Name = "Hotel Room Preference")]
        public string HotelRoomPreference { get; set; }

        [Display(Name = "1st Airline")]
        public string Airlinel1Name { get; set; }

        [Display(Name = "Frequent Flyer #")]
        public string Airline1MemberNumber { get; set; }

        [Display(Name = "2nd Airline")]
        public string Airline2Name { get; set; }

        [Display(Name = "Frequent Flyer #")]
        public string Airline2MemberNumber { get; set; }

        [Display(Name = "Airline Seating Preference")]
        public string AirlineSeatingPreference { get; set; }

        public string Status { get; set; }

        public bool ProfileComplete { get; set; }
        public bool DisclosureComplete { get; set; }
    }
}