using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event Code is required")]
        [Display(Name = "Event Code")]
        [MaxLength(10, ErrorMessage = "Maximum of 10 characters.")]
        public string EventCode { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Type of Event")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EventStartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EventEndDate { get; set; }

        //[RequiredIf("Enduring == false", ErrorMessage = "Required")]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        //[RequiredIf("Enduring == false", ErrorMessage = "Required")]
        [Display(Name = "Address 1")]
        public string VenueAddress1 { get; set; }

        [Display(Name = "Address 2")]
        public string VenueAddress2 { get; set; }

        //[RequiredIf("Enduring == false", ErrorMessage = "Required")]
        [Display(Name = "City")]
        public string VenueCity { get; set; }

        //[RequiredIf("Enduring == false", ErrorMessage = "Required")]
        [Display(Name = "State")]
        public string VenueState { get; set; }

        //[RequiredIf("Enduring == false", ErrorMessage = "Required")]
        [Display(Name = "Zip")]
        public string VenueZip { get; set; }

        [Display(Name = "Hotel Block Code")]
        public string HotelBlockCode { get; set; }

        [Display(Name = "Stay How Many Hours Before")]
        public int? ArriveHoursBefore { get; set; }

        [Display(Name = "Stay How Many Hours After")]
        public int? DepartHoursAfter { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Hotel Allowance")]
        public string HotelAllowance { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Airfare Allowance")]
        public string AirfareTrainAllowance { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Ground Transport Allowance")]
        public string GroundTransfersAllowance { get; set; }

        public ICollection<EventFaculty> EventUsers { get; set; }
        
        public string Status { get; set; }

        public User Manager { get; set; }
    }
}