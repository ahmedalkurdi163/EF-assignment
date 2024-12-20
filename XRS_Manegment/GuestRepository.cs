using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;

namespace XRS_Manegment
{
    public class GuestRepository : IRepository<Guest>
    {
        public void add(Guest item)
        {
            using (var Context = new XRS_Context())
            {
                // التحقق مما إذا كان معرف الفندق موجودًا في قاعدة البيانات
                var hotel = Context.Hotels.FirstOrDefault(h => h.Id == item.HotelId);
                if (hotel == null)
                {
                    //  اذا لم يتم ايجاد الفندق فلا يمكن اضافة الزائر الى قاعدة البيانات  
                    Console.WriteLine("Hotel not found!");
                    return;
                }
                // في حال كان الفندق موجود يتم اضافة بيانات الزائر 
                var newGuest = item;
                Context.Guests.Add(newGuest);
                Context.SaveChanges();

                /*  :  ملاحظة 
                         في اضافة الزائر بل يتم اضافته في اضافة الحجز وتحديث بيانات الزائر هناك  BookinId  لم يتم ادخال 
                 */
            }
        }

        public void delete(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var guest = Conntext.Guests.FirstOrDefault(e => e.Id == id);
                if (guest != null)
                {
                    guest.IsActive = false;
                    Conntext.Guests.Update(guest);
                    Conntext.SaveChanges();
                }
            }
        }

        public void print(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var guest = Conntext.Guests.FirstOrDefault(g => g.Id == id);
                if (guest != null)
                {
                    Console.WriteLine($"-------Iformation for Guest his Id {id}-------\n");
                    Console.WriteLine($"Id: {guest.Id}, Name: {guest.FirstName} {guest.LastName}\n");
                }
                else
                {
                    Console.WriteLine($"There is no Guest with this Id : {id}");
                }
            }
        }
        public void printAllItems()
        {
            using (var Conntext = new XRS_Context())
            {
                var guests = Conntext.Guests.Where(g => g.IsActive).ToList();
                foreach (var guest in guests)
                {
                    print(guest.Id);
                }
            }
        }
        public void update(int id, Guest item)
        {
            using (var Conntext = new XRS_Context())
            {
                var Guest = Conntext.Guests.FirstOrDefault(g => g.Id == id);
                if (Guest != null)
                {
                    Guest.IsActive = true;
                    Guest.HotelId = item.HotelId;
                    Guest.FirstName = item.FirstName;
                    Guest.LastName = item.LastName;
                    Guest.Email = item.Email;
                    Guest.BookingId = item.BookingId;
                    Guest.Phone = item.Phone;
                    Guest.DOB = item.DOB;

                    Conntext.Guests.Update(Guest);
                    Conntext.SaveChanges();
                } // Exception  في حال مالم يتم ايجاد الزائر يعطي 
                else
                {
                    throw new Exception("Guest is Not found !");
                }
            }
        }
    }
}
