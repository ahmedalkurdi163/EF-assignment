using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;

namespace XRS_Manegment
{
    public class BookingRepository : IRepository<Booking>
    {
        public void add(Booking item)
        {
            using (var Context = new XRS_Context())
            {
                // الاستعلام عن الزائر المراد الحجز له في حال كان موجود في قاعدة البيانات يتم الحجز
                var guest = Context.Guests.FirstOrDefault(g => g.Id == item.GuestId);
                if (guest == null)
                {
                    Console.WriteLine("Guest not found! Please provide a valid GuestId.");
                    return;
                }
                // التحقق مما إذا كان معرف الغرفة موجود في جدول الغرف
                var room = Context.Rooms.FirstOrDefault(r => r.Id == item.RoomId && r.Status == Status.Ready && r.IsActive);
                if (room == null)
                {
                    Console.WriteLine("Room not found or not ready! Please provide a valid RoomId.");
                    return;
                }
                // التحقق مما إذا كان معرف الموظف موجود في جدول الموظفين
                var employee = Context.Employees.FirstOrDefault(e => e.Id == item.EmployeeId && e.IsActive);
                if (employee == null)
                {
                    Console.WriteLine("Employee not found! Please provide a valid EmployeeId.");
                    return;
                }
                // إنشاء الحجز
                var booking = item;
                Context.Bookings.Add(booking);
                Context.SaveChanges();

                // اضافة عملية دفع اولية اما ان تكون كاملة او جزء من الدفع الكلي
                var payment = new Payment()
                {
                    BookingId = booking.Id,
                    CreateDate = booking.CheckinAt,
                    GuestId = item.GuestId,
                    IsActive = true,
                    ToutalAmount = item.Price,
                };
                Context.Payments.Add(payment);

                // تحديث حالة الغرفة من متاحة الى محجوزة
                room.Status = Status.occupied;
                room.BookingId = booking.Id;
                Context.Rooms.Update(room);

                // حفظ التغييرات في قاعدة البيانات
                Context.SaveChanges();

            }
        }

        public void delete(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var booking = Conntext.Bookings.FirstOrDefault(e => e.Id == id);
                if (booking != null)
                {
                    var room = Conntext.Rooms.FirstOrDefault(r => r.BookingId == booking.Id);
                    room.Status = Status.Ready;
                    room.BookingId = 0;
                    Conntext.Rooms.Update(room);
                    booking.IsActive = false;
                    Conntext.Bookings.Update(booking);
                    Conntext.SaveChanges();
                }
            }
        }

        public void print(int id)
        {
            using(var Conntext = new XRS_Context())
            {
                var booking = Conntext.Bookings.FirstOrDefault(b => b.Id == id);
                if(booking != null)
                {
                    Console.WriteLine($"-------Iformation for Bookin his Id {id}-------\n");
                    Console.WriteLine($"Id : {booking.Id} , CheckinAt :  {booking.CheckinAt} , CheckoutAt : {booking.CheckoutAt} ,Price : {booking.Price} \n");
                }
            }
        }

        public void printAllItems()
        {
            using (var Conntext = new XRS_Context())
            {
                var bookings = Conntext.Bookings.Where(b => b.IsActive).ToList();
                foreach (var b in bookings)
                {
                    print(b.Id);
                }
            }
        }

        public void update(int id, Booking item)
        {
            using (var Conntext = new XRS_Context())
            {
                var booking = Conntext.Bookings.FirstOrDefault(b => b.Id == id);
                if (booking != null)
                {
                    booking.IsActive = true;
                    booking.Price = item.Price;
                    booking.CheckinAt = item.CheckinAt;
                    booking.CheckoutAt = item.CheckoutAt;
                    booking.GuestId = item.GuestId;
                    booking.EmployeeId = item.EmployeeId;
                    booking.RoomId = item.RoomId;

                    Conntext.Bookings.Update(booking);
                    Conntext.SaveChanges();
                }
                else // Exception  في حال مالم يتم ايجاد الحجز يعطي 
                {
                    throw new Exception("Booking is Not found !");
                }
            }
        }
    }
}
