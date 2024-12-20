using Microsoft.AspNetCore.Http.HttpResults;
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
    public class HotelRepository : IHotelRepository
    {
        private readonly XRS_APPContext context;

        public HotelRepository(XRS_APPContext context) 
        {
            this.context = context;
        }
        public async Task<Hotel?> CreateHotelAsync(HotelForCreate hotel)
        {
            var newHotel = new Hotel()
            {
                Name = hotel.Name,
                Address = hotel.Address,
                Email = hotel.Email,
                Phone = hotel.Phone,
                IsActive = hotel.IsActive,
            };
            context.Hotels.Add(newHotel);
            await context.SaveChangesAsync(); 
            return newHotel;
        }

        public async Task<Hotel?> DeleteHotelAsync(int hotelId)
        {
            var hotel = context.Hotels.FirstOrDefault(h => h.Id == hotelId && h.IsActive);
            if (hotel == null) return null;

            hotel.IsActive = false;
            context.Hotels.Update(hotel);
            await context.SaveChangesAsync();
            return hotel;

        }

        public async Task<Hotel?> GetHotelAsync(int HotelId, bool includeInformation)
        {
            if (includeInformation)
                return await context.Hotels
                    .Include(h => h.Guests).Include(h => h.Employees).Include(h => h.Rooms)
                    .Where(h => h.Id == HotelId && h.IsActive).FirstOrDefaultAsync();
            else
                return await context.Hotels.Where(h => h.Id == HotelId && h.IsActive).FirstOrDefaultAsync();
        }

        public async Task<(List<Hotel>, PaginationMetaData)> GetHotelsAsync(int pageNumber, int pageSize, string keyword)
        {
            var hotels = new List<Hotel>();
            var totalItemCount = await context.Hotels.CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var quere = context.Hotels as IQueryable<Hotel>;
            if (!string.IsNullOrEmpty(keyword))
                quere = quere.Where(h => h.Name.ToLower().Contains(keyword) && h.IsActive);
           
            hotels = await quere.Where(h => h.IsActive).OrderBy(h => h.Name.ToLower())
                .Skip(pageSize*(pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (hotels, paginationdata);

        }

        public async Task<Hotel?> PatialyUpdateHotelAsync(int hotelId, JsonPatchDocument<HotelForUpdate> PatchDocument, ModelStateDictionary modelState)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null) return null;

            var hotelToPatch = new HotelForUpdate()
            {
                Address = hotel.Address,
                Employees = hotel.Employees,
                Email = hotel.Email,
                Guests = hotel.Guests,
                IsActive = hotel.IsActive,
                Name = hotel.Name,
                Phone = hotel.Phone,
                Rooms = hotel.Rooms,

            };

            PatchDocument.ApplyTo(hotelToPatch, modelState);

            if (!modelState.IsValid)
                return null;


            hotel.Rooms = hotelToPatch.Rooms;
            hotel.Address = hotelToPatch.Address;
            hotel.Email = hotelToPatch.Email;
            hotel.Employees = hotelToPatch.Employees;
            hotel.Guests = hotelToPatch.Guests;
            hotel.IsActive = hotelToPatch.IsActive;
            hotel.Name = hotelToPatch.Name;
            hotel.Phone = hotelToPatch.Phone;


            context.Hotels.Update(hotel);
            await context.SaveChangesAsync();

            return hotel;
        }
    }
}
