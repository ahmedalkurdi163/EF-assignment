namespace XRS_API.Models
{
    public class Employee
    {
        public int Id { get; set; }
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
