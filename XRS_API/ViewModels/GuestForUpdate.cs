using XRS_API.Models;

namespace XRS_API.ViewModels
{
    public class GuestForUpdate 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }
        public int HotelId { get; set; }
        public int? BookingId { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
