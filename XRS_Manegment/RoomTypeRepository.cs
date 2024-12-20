using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;

namespace XRS_Manegment
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        public void add(RoomType item)
        {
            using (var Conntext = new XRS_Context())
            {
                var RoomType = item;

                Conntext.Add(RoomType);
                Conntext.SaveChanges();
                var rooms = Conntext.Rooms.Where(r => r.RoomTypeId == item.Id).ToList();
                item.Rooms = rooms;
                Conntext.RoomTypes.Update(RoomType);
                Conntext.SaveChanges();

                
            }
        }

        public void delete(int id)
        {
            using(var  Conntext = new XRS_Context())
            {
                var roomtype = Conntext.RoomTypes.FirstOrDefault(r => r.Id == id);
                if(roomtype != null)
                {
                    roomtype.IsActive = false;
                    Conntext.RoomTypes.Update(roomtype);
                    Conntext.SaveChanges();
                }
            }
        }

        public void print(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var roomType = Conntext.RoomTypes.FirstOrDefault(r => r.Id == id);
                if (roomType != null)
                {
                    Console.WriteLine($"-------Iformation for RoomType his Id {id}-------\n");
                    Console.WriteLine($"Id : {roomType.Id}, TypeName : {roomType.TypeName}, NumOfBeds : {roomType.NumOfBeds}\n");
                }
            }
        }
        public void printAllItems()
        {
            using (var Conntext = new XRS_Context())
            {
                var roomTypes = Conntext.RoomTypes.Where(r => r.IsActive).ToList();
                foreach (var roomType in roomTypes)
                {
                    print(roomType.Id);
                }
            }
        }

        public void update(int id, RoomType item)
        {
            using (var Conntext = new XRS_Context())
            {
                var roomType = Conntext.RoomTypes.FirstOrDefault(r => r.Id == id);
                if (roomType != null)
                {
                    roomType.IsActive = true;
                    roomType.NumOfBeds = item.NumOfBeds;
                    roomType.TypeName = item.TypeName;

                    Conntext.RoomTypes.Update(roomType);
                    Conntext.SaveChanges();
                }
                else // Exception  في حال مالم يتم ايجاد نوع الغرفة  يعطي 
                {
                    throw new Exception("RoomTtype is Not found !");
                }
            }
        }
    }
}
