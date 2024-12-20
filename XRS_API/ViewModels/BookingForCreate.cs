using System.ComponentModel.DataAnnotations.Schema;
using XRS_API.Models;

namespace XRS_API.ViewModels
{
    public class BookingForCreate
    {
        public DateTime BookingDate { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckinAt { get; set; }
        public DateTime CheckoutAt { get; set; }
        public decimal Price { get; set; }
        public int EmployeeId { get; set; }
        public int GuestId { get; set; }
        public int HotelId { get; set; }
    }
}
