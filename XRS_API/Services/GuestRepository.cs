using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using XRS_API.DbContexts;
using XRS_API.Models;
using XRS_API.Models.ViewModels;
using XRS_API.ViewModels;

namespace XRS_API.Services
{
    public class GuestRepository : IGuestRepository
    {
        private readonly XRS_APPContext context;
        public GuestRepository(XRS_APPContext context)
        {
            this.context = context;
        }

        public async Task<Guest?> CreateGuestAsync(int HotelId, GuestForCreate guest)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == HotelId);
            if (hotel == null)
                return null;
            var newGuest = new Guest()
            {
                DOB = guest.DOB,
                Email = guest.Email,
                FirstName = guest.FirstName,
                HotelId = HotelId,
                IsActive = guest.IsActive,
                LastName = guest.LastName,
                Phone = guest.Phone,
            };

            context.Guests.Add(newGuest);
            await context.SaveChangesAsync();
            return newGuest;
        }

        public async Task<Guest?> DeleteGuestAsync(int hotelId, int guestId)
        {
            var hotel = context.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null) return null;


            var guest = await context.Guests.FirstOrDefaultAsync(g => g.Id == guestId && g.HotelId == hotelId && g.IsActive);

            if (guest == null) return null;
            guest.IsActive = false;

            context.Guests.Update(guest);
            await context.SaveChangesAsync();
            return guest;
        }

        public async Task<Guest?> GetGuestAsync(int HotelId, int guestId, bool? includePayments = false)
        {
            if((bool)includePayments)
                return await context.Guests.Include(g => g.Payments).FirstOrDefaultAsync(g => g.HotelId == HotelId && g.Id == guestId && g.IsActive);
            return await context.Guests.FirstOrDefaultAsync(g => g.HotelId == HotelId && g.Id == guestId && g.IsActive);

        }

        public async Task<(List<Guest>, PaginationMetaData)> GetGuestsAsync(int hotelId, int pageNumber, int pageSize, string? name)
        {
            var guests = new List<Guest>();
            var totalItemCount = await context.Guests.CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var quere = context.Guests as IQueryable<Guest>;
            if (!string.IsNullOrEmpty(name))
                quere = quere.Where(g => g.FirstName.ToLower().Contains(name) && g.IsActive);
            guests = await quere.Where(g => g.IsActive).OrderBy(g => g.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (guests, paginationdata);

        }

        public async Task<Guest?> PatialyUpdateGuestAsync(int hotelId, int guestId, JsonPatchDocument<GuestForUpdate> patchDocument, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null) return null;

            var guest1 = await context.Guests.FirstOrDefaultAsync(e => e.Id == guestId && e.HotelId == hotelId);
            if (guest1 == null) return null;

            var guestToPatch = new GuestForUpdate()
            {
                HotelId = hotelId,
                DOB = guest1.DOB,
                Email = guest1.Email,
                FirstName = guest1.FirstName,
                LastName = guest1.LastName,
                IsActive = guest1.IsActive,
                BookingId = guest1.BookingId,
                Payments = guest1.Payments,
                Phone = guest1.Phone
            };

            patchDocument.ApplyTo(guestToPatch, modelState);

            if (!modelState.IsValid)
            {
                return null;
            }

            guest1.FirstName = guestToPatch.FirstName;
            guest1.LastName = guestToPatch.LastName;
            guest1.DOB = guestToPatch.DOB;
            guest1.Email = guestToPatch.Email;
            guest1.IsActive = guestToPatch.IsActive;
            guest1.Phone = guestToPatch.Phone;
            guest1.BookingId = guestToPatch.BookingId;
            guest1.Payments = guestToPatch.Payments;


            context.Guests.Update(guest1);
            await context.SaveChangesAsync();

            return guest1;
        }
    }
}

