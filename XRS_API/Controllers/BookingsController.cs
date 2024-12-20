using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using XRS_API.Models;
using XRS_API.Services;
using XRS_API.ViewModels;

[Route("api/Hotels/{hotelId}/bookings")]
[ApiController]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository bookingRepository;
   
    private readonly IMapper mapper;
    private readonly int maxPageSize = 10;

    public BookingsController(IBookingRepository bookingRepository, IMapper mapper)
    {
        this.bookingRepository = bookingRepository;
        this.mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Booking>>> GetBookings(int hotelId, int pageNumber, int pageSize)
    {
        if (pageSize > maxPageSize)
            pageSize = maxPageSize;

        var (bookings, paginationMetaData) = await bookingRepository.GetBookingsAsync(hotelId, pageNumber, pageSize);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
       
        
        return Ok(bookings);
    }
    [AllowAnonymous]
    [HttpGet("{bookingId}", Name = "GetBooking")]
    public async Task<ActionResult<Booking>> GetBooking(int hotelId, int bookingId)
    {
        var booking = await bookingRepository.GetBookingAsync(hotelId, bookingId);
        if (booking == null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<Booking?>> CreateBooking(int hotelId, BookingForCreate booking)
    {
        var newBooking = await bookingRepository.CreateBookingAsync(hotelId, booking);

        if (newBooking == null)
        {
            return Conflict("The room is already booked for the given period.");
        }
        
        return CreatedAtRoute("GetBooking", new { hotelId = hotelId, bookingId = newBooking.Id }, newBooking);
    }

    [HttpDelete("{bookingId}")]
    public async Task<ActionResult<Booking>> DeleteBooking(int hotelId, int bookingId)
    {
        var booking = await bookingRepository.DeleteBookingAsync(hotelId, bookingId);
        if (booking == null)
            return NotFound();
        return NoContent();
    }
    [HttpPatch("{bookingId}")]
    public async Task<ActionResult<Booking>> PatialyUpdateBooking(int hotelId, int bookingId, [FromBody] JsonPatchDocument<BookingForUpdate> patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest();
        }

        var booking = await bookingRepository.PatialyUpdateBookingAsync(hotelId, bookingId, patchDocument, ModelState);
        if (booking == null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NotFound();
        }

        return NoContent();
    }
}
