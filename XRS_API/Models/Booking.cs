using System.ComponentModel.DataAnnotations.Schema;

namespace XRS_API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckinAt { get; set; }
        public DateTime CheckoutAt { get; set; }
        public decimal Price { get; set; }
        public int EmployeeId { get; set; }
        public int GuestId { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public int HotelId { get; set; }

    }
}
