using XRS_API.Models;

namespace XRS_API.ViewModels
{
    public class BookingForUpdate
    {
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
