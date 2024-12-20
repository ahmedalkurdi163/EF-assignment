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
    public class RoomRepository : IRoomRepository
    {
        private readonly XRS_APPContext context;
        public RoomRepository(XRS_APPContext context)
        {
            this.context = context;
        }

        public async Task<Room?> CreateRoomAsync(int HotelId, RoomForCreate room)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == HotelId);
            if (hotel == null)
                return null;

            var newRoom = new Room()
            {
                FloorNumber = room.FloorNumber,
                IsActive = room.IsActive,
                HotelId = HotelId,
                Number = room.Number,
                RoomTypeId = room.RoomTypeId,
                Status = room.Status,
            };

            context.Rooms.Add(newRoom);
            await context.SaveChangesAsync();
            return newRoom;
        }

        public async Task<Room?> DeleteRoomAsync(int hotelId, int roomId)
        {
            var hotel = context.Hotels.FirstOrDefault(h => h.Id == hotelId && h.IsActive);
            if (hotel == null) return null;


            var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.HotelId == hotelId && r.IsActive);

            if (room == null) return null;
            room.IsActive = false;

            context.Rooms.Update(room);
            await context.SaveChangesAsync();
            return room;
        }

        public async Task<Room?> GetRoomAsync(int HotelId, int RoomId)
        {
            return await context.Rooms.FirstOrDefaultAsync(r => r.HotelId == HotelId && r.Id == RoomId && r.IsActive);
        }

        public async Task<(List<Room>, PaginationMetaData)> GetRoomsAsync(int hotelId, int pageNumber, int pageSize, string? typename)
        {
            var rooms = new List<Room>();
            var totalItemCount = await context.Rooms.CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var quere = context.Rooms as IQueryable<Room>;
            if(!string.IsNullOrEmpty(typename)) 
                quere = quere.Where(r => r.RoomType.TypeName.ToLower().Contains(typename) && r.IsActive);
            rooms = await quere.Where(r => r.IsActive).OrderBy(r => r.Number)
                 .Skip(pageSize * (pageNumber - 1))
                 .Take(pageSize)
                 .ToListAsync();
            return (rooms, paginationdata);
        }

        public async Task<Room?> PatialyUpdateRoomAsync(int hotelId, int roomId, JsonPatchDocument<RoomForUpdate> PatchDocument, ModelStateDictionary modelState)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null) return null;

            var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.HotelId == hotelId);
            if (room == null) return null;

            var roomToPatch = new RoomForUpdate()
            {
                BookingId = room.BookingId,
                FloorNumber = room.FloorNumber,
                Number = room.Number,
                HotelId = room.HotelId,
                IsActive = room.IsActive,
                RoomTypeId = room.RoomTypeId,
                Status = room.Status
            };

            PatchDocument.ApplyTo(roomToPatch, modelState);

            if (!modelState.IsValid)
                return null;
            room.BookingId = roomToPatch.BookingId;
            room.FloorNumber = roomToPatch.FloorNumber;
            room.Number = roomToPatch.Number;
            room.HotelId = roomToPatch.HotelId;
            room.IsActive = roomToPatch.IsActive;
            room.RoomTypeId = roomToPatch.RoomTypeId;
            room.Status = roomToPatch.Status;

            context.Rooms.Update(room);
            await context.SaveChangesAsync();

            return room;

        }
    }
}
