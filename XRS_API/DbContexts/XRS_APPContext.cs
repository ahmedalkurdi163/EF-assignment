using Microsoft.EntityFrameworkCore;
using XRS_API.Models;

namespace XRS_API.DbContexts
{
    public class XRS_APPContext : DbContext
    {
        public XRS_APPContext(DbContextOptions<XRS_APPContext> Options) : base(Options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تعيين نوع الأعمدة للخصائص العشرية
            modelBuilder.Entity<Booking>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18, 2)"); // تحديد نوع العمود للخاصية Price

            modelBuilder.Entity<Payment>()
                .Property(p => p.ToutalAmount)
                .HasColumnType("decimal(18, 2)"); // تحديد نوع العمود للخاصية TotalAmount

            //----------------------------------
            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { Id = 1, IsActive = true, NumOfBeds = 1, TypeName = "P-01" },
                new RoomType { Id = 2, IsActive = true, NumOfBeds = 2, TypeName = "P-02" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, IsActive = true, FloorNumber = 1, HotelId = 1, Number = 1, RoomTypeId = 1, Status = Status.Ready }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    IsActive = true,
                    FirstName = "Ali",
                    LastName = "AlAli",
                    DOB = new DateTime(2000, 2, 12),
                    Email = "Alilllll@gmail.com",
                    StratDate = new DateTime(2024, 2, 1),
                    HotelId = 1,
                    Title = "Receptionist"
                }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "AK-07Hotels",
                    Address = "Azaz",
                    Email = "AK-07Hotels@hotel.com",
                    IsActive = true,
                    Phone = "12332131233",
                }
            );
        }
    }
}
