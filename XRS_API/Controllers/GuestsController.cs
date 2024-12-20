using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using XRS_API.Models;
using XRS_API.Models.ViewModels;
using XRS_API.Services;
using XRS_API.ViewModels;

namespace XRS_API.Controllers
{
    [Route("api/Hotels/{hotelId}/guests")]
    [ApiController]
    [Authorize]
    public class GuestsController : ControllerBase
    {
        private ILogger<GuestsController> logger;  
        private readonly IGuestRepository guestRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly int maxPageSize = 10;
        public GuestsController(IGuestRepository guestRepository,
            IMapper mapper, ILogger<GuestsController> logger)
        {

            this.guestRepository = guestRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Guest>>> GetGuests(int hotelId, int pageNumber, int pageSize, string? name)
        {
            try
            {
                if (pageSize > maxPageSize)
                    pageSize = maxPageSize;
                var (guest, paginationMetaData) = await guestRepository.GetGuestsAsync(hotelId, pageNumber, pageSize, name);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

                return Ok(mapper.Map<List<Guest_withOutOverData>>(guest));
            }
            catch (Exception ex)
            {
                this.logger.LogCritical("Unhandled exception : error happend", ex.Message);
                return StatusCode(500, "Somthing worng");
            }
        }
        [AllowAnonymous]
        [HttpGet("{guestId}",Name = "GetGuest")]
        public async Task<ActionResult<Guest>> GetGuest(int HotelId, int guestId, bool? includePayments = false)
        {
            try
            {
                var guest = await guestRepository.GetGuestAsync(HotelId, guestId, includePayments);
                if (guest == null)
                {
                    this.logger.LogWarning($"guest {guestId} couldn't be found");
                    return NotFound();
                }
                   
                if ((bool)includePayments)
                    return Ok(guest);
                return Ok(mapper.Map<Guest_withOutOverData>(guest));
            }
            catch (Exception ex)
            {
                this.logger.LogCritical("Unhandled exception : error happend", ex.Message);
                return StatusCode(500, "Somthing worng");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guest?>> CreatGuest(int hotelId, GuestForCreate guest)
        {
            try
            {
                var newguest = await guestRepository.CreateGuestAsync(hotelId, guest);
                if (newguest == null)
                {
                    this.logger.LogWarning($"There is Some Error Here ");
                    return BadRequest();
                }
                return CreatedAtRoute("GetGuest", new { HotelId = hotelId, guestId = newguest.Id, includePayments = false }, mapper.Map<Guest_withOutOverData>(newguest));
            }
            catch (Exception ex)
            {
                this.logger.LogCritical("Unhandled exception : error happend", ex.Message);
                return StatusCode(500, "Somthing worng");
            }
        }
        [HttpDelete("{guestId}")]
        public async Task<ActionResult<Guest>> DeleteGuest(int hotelId, int guestId)
        {
            try
            {
                var guest = await guestRepository.DeleteGuestAsync(hotelId, guestId);
                if (guest == null)
                {
                    this.logger.LogWarning($"There is Some Error Here ");
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogCritical("Unhandled exception : error happend", ex.Message);
                return StatusCode(500, "Somthing worng");
            }
        }

        [HttpPatch("{guestId}")]
        public async Task<ActionResult<Employee>> PatialyUpdateEmployee(int hotelId, int guestId, [FromBody] JsonPatchDocument<GuestForUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var guest = await guestRepository.PatialyUpdateGuestAsync(hotelId, guestId, patchDocument, ModelState);
            if (guest == null)
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
}
