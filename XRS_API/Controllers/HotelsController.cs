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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly HotelWithOutListsInfo hotelWithOutListsInfo;
        private readonly int maxPageSize = 10;

        public HotelsController(IConfiguration configuration,
                    IHotelRepository hotelRepository,
                    IMapper mapper , HotelWithOutListsInfo hotelWithOutListsInfo) 
        {
            this.hotelRepository = hotelRepository;
            this.configuration = configuration;
            this.mapper = mapper;
            this.hotelWithOutListsInfo = hotelWithOutListsInfo;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetHotels(int pageNumber = 1, int pageSize = 10 , string? keyword = null)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;
            var (hotels, paginationMetaData) = await hotelRepository.GetHotelsAsync(pageNumber, pageSize, keyword);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            var hotelWithOutListInformation = mapper.Map<List<HotelWithOutListsInfo>>(hotels);

            return Ok(hotelWithOutListInformation);
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name ="GetHotel")]
        public async Task<ActionResult<Hotel>> GetHotel(int id , bool includeinfo)
        {
            var hotel = await hotelRepository.GetHotelAsync(id, includeinfo);
            if (hotel == null)
                return NotFound();
            if(includeinfo)
                return Ok(hotel);
            return Ok(mapper.Map<HotelWithOutListsInfo>(hotel));
        }
        [HttpPost]
        public async Task<ActionResult<Hotel?>> CreatHotel(HotelForCreate hotel)
        {
            var newHotel = await hotelRepository.CreateHotelAsync(hotel);
            if (newHotel == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetHotel", new { id = newHotel.Id }, mapper.Map<HotelWithOutListsInfo>(newHotel));
        }
        [HttpDelete("{hotelId}")]
        public async Task<ActionResult<Employee>> DeleteHotel(int hotelId)
        {
            var hotel = await hotelRepository.DeleteHotelAsync(hotelId);
            if (hotel == null)
                return NotFound();
            return NoContent();
        }
        [HttpPatch("{hotelId}")]
        public async Task<ActionResult<Hotel>> PatialyUpdateHotel(int hotelId,  [FromBody] JsonPatchDocument<HotelForUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var hotel = await hotelRepository.PatialyUpdateHotelAsync(hotelId, patchDocument, ModelState);
            if (hotel == null)
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
