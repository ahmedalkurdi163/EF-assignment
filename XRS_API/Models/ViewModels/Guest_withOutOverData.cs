namespace XRS_API.Models.ViewModels
{
    public class Guest_withOutOverData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }
        public int HotelId { get; set; }
        public int? BookingId { get; set; }

    }
}
