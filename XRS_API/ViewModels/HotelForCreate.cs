using XRS_API.Models;

namespace XRS_API.ViewModels
{
    public class HotelForCreate
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

    }
}
