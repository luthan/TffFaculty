using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AdminEventFacultyViewModel
    {
        public string Id { get; set; }

        public bool BookHotel { get; set; }

        [Display(Name = "Check In")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckIn { get; set; }

        [Display(Name = "Check Out")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CheckOut { get; set; }

        //[Display(Name = "Room Preference")]
        //public string RoomPreference { get; set; }

        //public bool SpecialAccess { get; set; }

        //[Display(Name = "Hotel Location Preference")]
        //public string HotelLocationPreference { get; set; }

        [Display(Name = "Travel Method")]
        public string TravelMethod { get; set; }

        [Display(Name = "Departure Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DepartureDate { get; set; }

        [Display(Name = "Return Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Departure Airport / Train Station")]
        public string DepartureLocation { get; set; }

        [Display(Name = "Approximate departure time preference")]
        public string DepartureTime { get; set; }

        [Display(Name = "Return Airport / Train Station")]
        public string ReturnLocation { get; set; }

        [Display(Name = "Approximate return time preference")]
        public string ReturnTime { get; set; }

        public bool Completed { get; set; }
        public bool FilledOut { get; set; }

        public bool Inactive { get; set; }

        public bool Invited { get; set; }

        public DateTime? InviteDate { get; set; }

        [Required(ErrorMessage = "Need at least one role")]
        [Display(Name = "Roles")]
        public List<String> EventFacultyRoles { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}