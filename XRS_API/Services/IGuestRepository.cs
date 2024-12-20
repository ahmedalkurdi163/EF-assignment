using XRS_API.Models.ViewModels;
using XRS_API.Models;
using XRS_API.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XRS_API.Services
{
    public interface IGuestRepository
    {
        Task<(List<Guest>, PaginationMetaData)> GetGuestsAsync(int hotelId, int pageNumber, int pageSize, string? name);
        Task<Guest?> GetGuestAsync(int HotelId, int guestId , bool? includePayments);

        Task<Guest?> CreateGuestAsync(int HotelId, GuestForCreate guest);
        Task<Guest?> DeleteGuestAsync(int hotelId, int guestId);
        Task<Guest?> PatialyUpdateGuestAsync(int hotelId, int guestId, JsonPatchDocument<GuestForUpdate> patchDocument, ModelStateDictionary modelState);

    }
}
