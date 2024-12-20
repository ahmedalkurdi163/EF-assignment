using XRS_API.Models;

namespace XRS_API.ViewModels
{
    public class EmployeeForCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public DateTime StratDate { get; set; } = new DateTime();
        public int HotelId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
