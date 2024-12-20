using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRS_Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckinAt { get; set; }
        public DateTime CheckoutAt { get; set; }
        public decimal Price { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int GuestId { get; set; }
        [ForeignKey("GuestId")]
        public Guest Guest { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();

    }
}
