using Microsoft.EntityFrameworkCore.Migrations;
using XRS_Data;
using XRS_Domain;
using XRS_Manegment;

namespace Ahmed_Alkurdi.XRS
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, World!\n");

            Ensure_Created();

            IRepository<Hotel> Hotelrepository = new HotelRepository();

            IRepository<Employee> Employeerepository = new EmployeeRepository();

            IRepository<RoomType> RoomTyperepository = new RoomTypeRepository();

            IRepository<Room> Roomrepository = new RoomRepository();

            IRepository<Guest> Guestrepository = new GuestRepository();

            IRepository<Booking> Bookingrepository = new BookingRepository();

            var Hotel1 = new Hotel()
            {
                IsActive = true,
                Address = "Azaz",
                Email = "AK_07@gmail.com",
                Name = "AK_07",
                Phone = "0092987982792",
            };

            var Employee1 = new Employee()
            {
                FirstName = "jjj",
                LastName = "fff",
                Email = "hhh@gmail.com",
                DOB = new DateTime(2002, 2, 2),
                HotelId = 1,
                IsActive = true,
                StratDate = new DateTime(2020, 1, 1),
                Title = "Food service employee",

            };

            var RoomType1 = new RoomType()
            {
                IsActive = true,
                NumOfBeds = 2,
                TypeName = "jdnk"
            };

            var Room1 = new Room()
            {
                IsActive = true,
                FloorNumber = 1,
                HotelId = 1,
                RoomTypeId = 1,
                Number = 1,
                Status = Status.Ready,

            };

            var Guest1 = new Guest()
            {
                FirstName = "ahmed",
                LastName = "alkurdi",
                DOB = new DateTime(2020, 2, 12),
                Email = "kjhdkjsh ",
                HotelId = 1,
                IsActive = true,
                Phone = "9340348085403",
            };

            var Booking1 = new Booking()
            {
                BookingDate = new DateTime(2020, 2, 12),
                CheckinAt = new DateTime(2020, 2, 12),
                CheckoutAt = new DateTime(2020, 2, 12),
                GuestId = 1,
                RoomId = 1,
                IsActive = true,
                Price = 120,
                EmployeeId = 1,
            };
            Hotelrepository.add(Hotel1);

            Employeerepository.add(Employee1);

            RoomTyperepository.add(RoomType1);

            Roomrepository.add(Room1);

            Guestrepository.add(Guest1);

            Bookingrepository.add(Booking1);

            // طباعة عنصر محدد

            Hotelrepository.print(1);

            Employeerepository.print(1);

            Guestrepository.print(1);

            //----------------------------
            //اضافة 

            Hotelrepository.add(new Hotel()
            {
                IsActive = true,
                Address = "Efrin/Halep/Syria",
                Email = "AK_07Ef@gmail.com",
                Name = "EfrinHotel",
                Phone = "0092987982792",
            });

            Employeerepository.add(new Employee()
            {
                FirstName = "hamed",
                LastName = "abohmid",
                Email = "abohmid@gmail.com",
                DOB = new DateTime(2002, 2, 2),
                HotelId = 2,
                IsActive = true,
                StratDate = new DateTime(2020, 1, 1),
                Title = "Receptionist",

            });

            RoomTyperepository.add(new RoomType()
            {
                IsActive = true,
                NumOfBeds = 2,
                TypeName = "A-P2"
            });

            Roomrepository.add(new Room()
            {
                IsActive = true,
                FloorNumber = 1,
                HotelId = 1,
                RoomTypeId = 2,
                Number = 2,
                Status = Status.Ready,

            });

            Guestrepository.add(new Guest()
            {
                FirstName = "ahmed",
                LastName = "alkurdi",
                DOB = new DateTime(2020, 2, 12),
                Email = "alkurdi@gmail.com",
                HotelId = 1,
                IsActive = true,
                Phone = "9340348085403",

            });

            Bookingrepository.add(new Booking()
            {
                BookingDate = new DateTime(2020, 2, 12),
                CheckinAt = new DateTime(2020, 2, 12),
                CheckoutAt = new DateTime(2020, 2, 12),
                GuestId = 2,
                RoomId = 2,
                IsActive = true,
                Price = 120,
                EmployeeId = 2,

            });

            Hotelrepository.print(2);

            Employeerepository.print(2);

            //----------------------------
            // تحديث المعلومات
            Hotelrepository.update(2, new Hotel()
            {
                IsActive = true,
                Address = "Efrin/Halep/Syria",
                Email = "AK_07Ef@gmail.com",
                Name = "EfrinHotel",
                Phone = "0092987982792",
            });

            Employeerepository.update(2, new Employee()
            {
                FirstName = "hamed",
                LastName = "updated",
                Email = "updated@gmail.com",
                DOB = new DateTime(2002, 2, 2),
                HotelId = 2,
                IsActive = true,
                StratDate = new DateTime(2020, 1, 1),
                Title = "Receptionist",

            });

            RoomTyperepository.update(2, new RoomType()
            {
                IsActive = true,
                NumOfBeds = 2,
                TypeName = "A-P2"
            });

            Roomrepository.update(2, new Room()
            {
                IsActive = true,
                FloorNumber = 1,
                HotelId = 1,
                RoomTypeId = 2,
                Number = 2,
                Status = Status.Ready,

            });

            Guestrepository.update(2, new Guest()
            {
                FirstName = "updated",
                LastName = "test",
                DOB = new DateTime(2002, 2, 12),
                Email = "updated@gmail.com",
                HotelId = 1,
                IsActive = true,
                Phone = "9340348085403",

            });

            Bookingrepository.update(2, new Booking()
            {
                BookingDate = new DateTime(2024, 4, 4),
                CheckinAt = new DateTime(2024, 4, 4),
                CheckoutAt = new DateTime(2024, 5, 12),
                GuestId = 2,
                RoomId = 2,
                IsActive = true,
                Price = 120,
                EmployeeId = 2,

            });
            //--------------------------------
            //  عرض جميع العناصر لشيء معين
            Hotelrepository.printAllItems();

            Employeerepository.printAllItems();

            RoomTyperepository.printAllItems();

            Roomrepository.printAllItems();

            Guestrepository.printAllItems();

            Bookingrepository.printAllItems();


            //------------------------
            // حذف
            Hotelrepository.delete(2);

            Employeerepository.delete(2);

            RoomTyperepository.delete(2);

            Roomrepository.delete(2);

            Guestrepository.delete(2);

            Bookingrepository.delete(2);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
        public static void Ensure_Created()
        {
            using (var Context = new XRS_Context())
            {
                Context.Database.EnsureCreated();
            }
        }

    }
}
