namespace XRS_API.Models.ViewModels
{
    public class Room_withOutOverData
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int FloorNumber { get; set; }
        public Status Status { get; set; }
        public int HotelId { get; set; }
        public bool IsActive { get; set; } = true;
        public int RoomTypeId { get; set; }
        public int BookingId { get; set; }

    }
}
