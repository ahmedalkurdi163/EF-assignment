using Microsoft.EntityFrameworkCore;
using XRS_Domain;

namespace XRS_Data
{
    public class XRS_Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = HotelDB1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تحديد العلاقة بين الحجز والزائر
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest) // الحجز له زائر واحد
                .WithOne(g => g.Booking) // الزائر له حجز واحد
                .HasForeignKey<Guest>(g => g.BookingId)
                .OnDelete(DeleteBehavior.Restrict); // لن يتم حذف الحجز حتى يحذف الزائر

            modelBuilder.Entity<Booking>() // علاقة الحجز مع الغرفة وهي واحد لواحد
                .HasOne(b => b.Room)
                .WithOne(r => r.Booking)
                .HasForeignKey<Booking>(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // ايضا لن يتم حذف الحجز حتى يتم الغاء ارتباطه مع الغرفة

            modelBuilder.Entity<Hotel>() // الفندق له العديد من الغرف
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hotel>() // الفندق له العديد من الموظفين
                .HasMany(h => h.Employees)
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hotel>() // الفندق له العديد من الزوار
                .HasMany(h => h.Guests)
                .WithOne(g => g.Hotel)
                .HasForeignKey(g => g.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>() // الزائر له العديد من عمليات الدفع
                .HasOne(p => p.Guest)
                .WithMany(g => g.Payments)
                .HasForeignKey(p => p.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            // تعيين نوع الأعمدة للخصائص العشرية
            modelBuilder.Entity<Booking>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18, 2)"); // تحديد نوع العمود للخاصية Price

            modelBuilder.Entity<Payment>()
                .Property(p => p.ToutalAmount)
                .HasColumnType("decimal(18, 2)"); // تحديد نوع العمود للخاصية TotalAmount
        }

        public DbSet<Hotel> Hotels { get; set; } // اضافة جدول الفنادق
        public DbSet<Employee> Employees { get; set; } // اضافة جدول الموظفين
        public DbSet<Room> Rooms { get; set; } // اضافة جدول الغرف
        public DbSet<RoomType> RoomTypes { get; set; } // اضافة جدول انواع الغرف
        public DbSet<Guest> Guests { get; set; } // اضافة جدول الزوار
        public DbSet<Booking> Bookings { get; set; } // اضافة جدول الحجوزات
        public DbSet<Payment> Payments { get; set; } // اضافة جدول عمليات الدفع
    }
}
