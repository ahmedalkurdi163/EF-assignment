using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRS_Domain
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int FloorNumber { get; set; }
        public Status Status { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public bool IsActive { get; set; } = true;
        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

    }
    public enum Status
    {
        Ready,
        occupied,
        dirty,
        out_of_order,
    }

}
