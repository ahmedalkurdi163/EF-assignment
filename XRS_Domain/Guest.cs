using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRS_Domain
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int? BookingId { get; set; }
        public Booking Booking { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();

    }
}
