using XRS_API.Models.ViewModels;
using XRS_API.Models;
using XRS_API.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace XRS_API.Services
{
    public interface IBookingRepository
    {
        Task<(List<Booking>, PaginationMetaData)> GetBookingsAsync(int hotelId, int pageNumber, int pageSize);
        Task<Booking?> GetBookingAsync(int HotelId, int bookingId);

        Task<Booking?> CreateBookingAsync(int HotelId, BookingForCreate booking);

        Task<Booking?> DeleteBookingAsync(int hotelId, int bookingId);

        Task<Booking?> PatialyUpdateBookingAsync(int hotelId, int bookingId, JsonPatchDocument<BookingForUpdate> PatchDocument, ModelStateDictionary modelState);
    }
}
