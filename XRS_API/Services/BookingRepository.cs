using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using XRS_API.DbContexts;
using XRS_API.Models;
using XRS_API.Models.ViewModels;
using XRS_API.ViewModels;

namespace XRS_API.Services
{
    public class BookingRepository : IBookingRepository
    {
        private readonly XRS_APPContext context;
        public BookingRepository(XRS_APPContext context)
        {
            this.context = context;
        }

        public async Task<Booking?> CreateBookingAsync(int hotelId, BookingForCreate booking)
        {
            // التحقق من وجود الفندق
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null)
                return null;

            // التحقق من أن الغرفة جاهزة
            var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == booking.RoomId && r.Status == Status.Ready);
            if (room == null)
                return null;

            // التحقق من عدم وجود حجز متداخل لنفس الغرفة
            var conflictingBooking = await context.Bookings
                .FirstOrDefaultAsync(b => b.RoomId == booking.RoomId);

            if (conflictingBooking != null)
            {
                return null;
            }

            // إنشاء حجز جديد
            var newBooking = new Booking()
            {
                HotelId = hotelId,
                IsActive = booking.IsActive,
                BookingDate = booking.BookingDate,
                CheckinAt = booking.CheckinAt,
                CheckoutAt = booking.CheckoutAt,
                EmployeeId = booking.EmployeeId,
                GuestId = booking.GuestId,
                Price = booking.Price,
                RoomId = booking.RoomId,
            };

            context.Bookings.Add(newBooking);
            await context.SaveChangesAsync();
            return newBooking;
        }

        public async Task<Booking?> DeleteBookingAsync(int hotelId, int bookingId)
        {
            var hotel = context.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null) return null;


            var booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.HotelId == hotelId && b.IsActive);

            if (booking == null) return null;
            booking.IsActive = false;

            context.Bookings.Update(booking);
            await context.SaveChangesAsync();
            return booking;

        }

        public async Task<Booking?> GetBookingAsync(int HotelId, int bookingId)
        {
            return await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.HotelId == HotelId && b.IsActive);
        }

        public async Task<(List<Booking>, PaginationMetaData)> GetBookingsAsync(int hotelId, int pageNumber, int pageSize)
        {

            var bookings = new List<Booking>();
            var totalItemCount = await context.Bookings.CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var quere = context.Bookings as IQueryable<Booking>;

            bookings = await quere.Where(b => b.HotelId == hotelId && b.IsActive)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (bookings, paginationdata);
        }

        public async Task<Booking?> PatialyUpdateBookingAsync(int hotelId, int bookingId, JsonPatchDocument<BookingForUpdate> PatchDocument, ModelStateDictionary modelState)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null) return null;

            var booking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.HotelId == hotelId );
            if (booking == null) return null;

            var bookingToPatch = new BookingForUpdate()
            {
                IsActive = booking.IsActive,
                BookingDate = booking.BookingDate,
                CheckinAt = booking.CheckinAt,
                CheckoutAt = booking.CheckoutAt,
                EmployeeId = booking.EmployeeId,
                GuestId = booking.GuestId,
                HotelId = booking.HotelId,
                Payments = booking.Payments,
                Price = booking.Price,
                RoomId = booking.RoomId
            };
            PatchDocument.ApplyTo(bookingToPatch, modelState);
            if (!modelState.IsValid)
                return null;
            booking.IsActive = bookingToPatch.IsActive;
            booking.BookingDate = bookingToPatch.BookingDate;
            booking.CheckinAt = bookingToPatch.CheckinAt;
            booking.CheckoutAt = bookingToPatch.CheckoutAt;
            booking.EmployeeId = bookingToPatch.EmployeeId;
            booking.GuestId = bookingToPatch.GuestId;
            booking.HotelId = bookingToPatch.HotelId;
            booking.Payments = bookingToPatch.Payments;
            booking.Price = bookingToPatch.Price;
            booking.RoomId = bookingToPatch.RoomId;


            context.Bookings.Update(booking);
            await context.SaveChangesAsync();

            return booking;

        }
    }
}
