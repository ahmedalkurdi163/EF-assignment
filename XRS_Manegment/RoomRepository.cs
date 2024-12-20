using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XRS_Manegment
{
    public class RoomRepository : IRepository<Room>
    {
        public void add(Room item)
        {
            using (var Conntext = new XRS_Context())
            {
                var RoomType = Conntext.RoomTypes.FirstOrDefault(r => r.Id == item.RoomTypeId);
                if (RoomType != null)
                {
                    var Room = item;
                    Conntext.Add(Room);
                    Conntext.SaveChanges();
                }
            }
        }

        public void delete(int id)
        {
            Console.WriteLine("Why Did you need To Delete a Room??????");
        }

        public void print(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var room = Conntext.Rooms.FirstOrDefault(r => r.Id == id);
                if (room != null)
                {
                    Console.WriteLine($"-------Iformation for Room his Id {id}-------\n");
                    Console.WriteLine($"Id : {room.Id}, Number : {room.Number}, FloorNumber : {room.FloorNumber}, Status : {room.Status}\n");
                }
            }
        }
        public void printAllItems()
        {
            using (var Conntext = new XRS_Context())
            {
                var rooms = Conntext.Rooms.Where(r => r.IsActive).ToList();
                foreach (var room in rooms)
                {
                    print(room.Id);
                }
            }
        }
        public void update(int id, Room item)
        {
            using (var Conntext = new XRS_Context())
            {
                var room = Conntext.Rooms.FirstOrDefault(r => r.Id == id);
                if (room != null)
                {
                    room.IsActive = true;
                    room.HotelId = item.HotelId;
                    room.RoomTypeId = item.RoomTypeId;
                    room.Number = item.Number;
                    room.Number = item.FloorNumber;
                    room.Status = item.Status;
                }// Exception  في حال مالم يتم ايجاد الغرفة يعطي 
                else
                {
                    throw new Exception("Ronn is Not found !");
                }
            }
        }
    }
}
