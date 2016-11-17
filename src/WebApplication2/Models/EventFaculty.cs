using System;

namespace WebApplication2.Models
{
    public class EventFaculty
    {
        public string Id { get; set; }
        public bool BookHotel { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        //public string RoomPreference { get; set; }
        //public bool SpecialAccess { get; set; }
        //public string HotelLocationPreference { get; set; }
        public string TravelMethod { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string DepartureLocation { get; set; }
        public string DepartureTime { get; set; }
        public string ReturnLocation { get; set; }
        public string ReturnTime { get; set; }

        public bool FilledOut { get; set; }
        public bool Completed { get; set; }

        public bool Inactive { get; set; }
        public bool Invited { get; set; }
        public DateTime? InviteDate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}