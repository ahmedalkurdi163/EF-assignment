using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRS_Domain
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public List<Guest> Guests { get; set; } = new List<Guest>();

        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
