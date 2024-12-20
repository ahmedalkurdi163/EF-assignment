namespace XRS_API.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int NumOfBeds { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
        public bool IsActive { get; set; } = true;
    }
}
