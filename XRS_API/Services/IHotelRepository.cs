using XRS_API.Models;
using XRS_API.Models.ViewModels;
using XRS_API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.JsonPatch;

namespace XRS_API.Services
{
    public interface IHotelRepository
    {
        Task<(List<Hotel>, PaginationMetaData)> GetHotelsAsync(int pageNumber, int pageSize, string name);
        Task<Hotel?> GetHotelAsync(int HotelId , bool includeInformation);

        Task<Hotel?> CreateHotelAsync(HotelForCreate hotel);

        Task<Hotel?> DeleteHotelAsync(int hotelId);
        Task<Hotel?> PatialyUpdateHotelAsync(int hotelId, JsonPatchDocument<HotelForUpdate> PatchDocument, ModelStateDictionary modelState);
    }
}
