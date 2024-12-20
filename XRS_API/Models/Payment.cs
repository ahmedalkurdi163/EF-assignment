namespace XRS_API.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal ToutalAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public int BookingId { get; set; }
        public int GuestId { get; set; }
    }
}
