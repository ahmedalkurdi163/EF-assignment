using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRS_Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal ToutalAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
        public Guest Guest { get; set; }
        public int GuestId { get; set; }
    }
}
