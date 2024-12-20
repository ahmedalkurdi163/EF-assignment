using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;

namespace XRS_Manegment
{
    public class HotelRepository : IRepository<Hotel>
    {
        public void add(Hotel item)
        {
            using (var Conntext = new XRS_Context())
            {
                var Hotel1 = item;
                // اضافة الاوتيل لقاعدة البيانات
                Conntext.Hotels.Add(Hotel1);
                // حفظ التغيرات التي تم اجرائها في قاعدة البيانات 
                Conntext.SaveChanges();
            }
        }

        public void delete(int id)
        {
            using(var Conntext = new XRS_Context())
            {
                // الاستعلام عن الاوتيل
                var Hotel = Conntext.Hotels
                    .Include(e => e.Employees).Include(e => e.Rooms)
                    .FirstOrDefault(e => e.Id == id && e.IsActive == true);
                if (Hotel != null)
                {
                    //  تغيير حالته من نشط الى غير نشط
                    Hotel.IsActive = false;
                    // تحديث معلوماته ضمن قاعدة البيانات
                    Conntext.Hotels.Update(Hotel);
                    // حفظ التغييرات
                    Conntext.SaveChanges();
                }
            }
        }

        public void print(int id)
        {
            using (var Conntext = new XRS_Context())
            {

                // الاستعلام عن الأوتيل محدد
                var hotel = Conntext.Hotels
                    .Include(e => e.Employees) // تضمين الموظفين مع الاستعلام
                    .Include(r => r.Rooms) // تضمين الغرف مع الاستعلام من اجل طباعة معلوماتها
                    .Include(g => g.Guests) // تضمين الزوار من اجل طباعة معلوماتهم
                    .FirstOrDefault(h => h.Id == id && h.IsActive == true); // الشرط خاص فيما اذا كان الأوتيل محذوف او لا

                if (hotel != null)
                {
                    Console.WriteLine($"-----Hotel {id} Information-----\n");
                    // طباعة معلومات الأوتيل
                    Console.WriteLine($"Id: {hotel.Id}, Name: {hotel.Name}, Address: {hotel.Address} \n");

                    // طباعة الموظفين الخاصين بالأوتيل
                    Console.WriteLine("Our Employees: ");
                    foreach (var i in hotel.Employees)
                    {
                        // فحص فيما اذا كان الموظف محذوف او لا
                        if (i.IsActive)
                            Console.WriteLine($"Id: {i.Id}, Name: {i.FirstName} {i.LastName} \n");
                    }

                    // طباعة الغرف الخاصة بالأوتيل
                    Console.WriteLine("Our Rooms: ");
                    foreach (var i in hotel.Rooms)
                    {
                        // فحص فيما اذا كانت الغرفة موجودة او لا
                        if (i.IsActive)
                            Console.WriteLine($"Id: {i.Id}, Number of room: {i.Number}, Status: {i.Status} \n");
                    }

                    // طباعة الزوار الموجودين في الأوتيل
                    Console.WriteLine("Our Guests: ");
                    foreach (var i in hotel.Guests)
                    {

                        // فحص فيما اذا كان الزائر محذوف او لا
                        if (i.IsActive)
                            Console.WriteLine($"Id: {i.Id}, Name: {i.FirstName} {i.LastName}  \n ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"There is no Hotel with this Id : {id}");
                }
            }

        }
        public void printAllItems()
        {
            using (var Conntext = new XRS_Context())
            {
                var hotels = Conntext.Hotels.Where(h => h.IsActive).ToList();
                foreach (var hotel in hotels)
                {
                    print(hotel.Id);
                }
            }
        }
        public void update(int id, Hotel item)
        {
            using (var Conntext = new XRS_Context())
            {
                var hotel = Conntext.Hotels.FirstOrDefault(h => h.Id == id);
                if (hotel != null)
                {
                    hotel.Name = item.Name;
                    hotel.Email = item.Email;
                    hotel.Address = item.Address;
                    hotel.Phone = item.Phone;
                    hotel.IsActive = true;

                    Conntext.Hotels.Update(hotel);
                    Conntext.SaveChanges();
                }// Exception  في حال مالم يتم ايجاد الفندق يعطي 
                else
                {
                    throw new Exception("Hotel is Not found !");
                }
            }
        }
    }
}
