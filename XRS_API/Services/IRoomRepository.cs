using XRS_API.Models.ViewModels;
using XRS_API.Models;
using XRS_API.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.JsonPatch;

namespace XRS_API.Services
{
    public interface IRoomRepository
    {
        Task<(List<Room>, PaginationMetaData)> GetRoomsAsync(int hotelId, int pageNumber, int pageSize, string? name);
        Task<Room?> GetRoomAsync(int HotelId, int RoomId);

        Task<Room?> CreateRoomAsync(int HotelId, RoomForCreate room);
        Task<Room?> DeleteRoomAsync(int hotelId, int roomId);
        Task<Room?> PatialyUpdateRoomAsync(int hotelId, int roomId, JsonPatchDocument<RoomForUpdate> PatchDocument, ModelStateDictionary modelState);
    }
}
