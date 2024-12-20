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
    [Authorize]
    [Route("api/Hotels/{hotelId}/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> logger;
        private readonly IRoomRepository roomRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly int maxPageSize = 10;
        public RoomsController(ILogger<RoomsController> _logger, 
             IRoomRepository roomRepository, IMapper mapper)
        {
            this.logger = _logger;
            this.roomRepository = roomRepository;
            this.mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetRooms(int hotelId, int pageNumber, int pageSize, string? name)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;
            var (rooms, paginationMetaData) = await roomRepository.GetRoomsAsync(hotelId, pageNumber, pageSize, name);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

           var room_withOutOverData = mapper.Map<List<Room_withOutOverData>>(rooms);

            return Ok(room_withOutOverData);
        }
        [AllowAnonymous]
        [HttpGet("{roomId}" ,Name ="GetRoom")]
        public async Task<ActionResult<Employee>> GetEmployee(int HotelId, int roomId)
        {
            var room = await roomRepository.GetRoomAsync(HotelId , roomId);
            if (room == null)
                return NotFound();
            return Ok(mapper.Map<Room_withOutOverData>(room));
        }

        [HttpPost]
        public async Task<ActionResult<Room?>> CreatRoom(int hotelId, RoomForCreate room)
        {
            var newRoom = await roomRepository.CreateRoomAsync(hotelId, room);
            if (newRoom == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetRoom", new { HotelId = hotelId, roomId = newRoom.Id }, mapper.Map<Room_withOutOverData>(newRoom));
        }
        [HttpDelete("{roomId}")]
        public async Task<ActionResult<Room>> DeleteRoom(int hotelId, int roomId)
        {
            var room = await roomRepository.DeleteRoomAsync(hotelId, roomId);
            if (room == null)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{roomId}")]
        public async Task<ActionResult<Room>> PatialyUpdateRoom(int hotelId, int roomId, [FromBody] JsonPatchDocument<RoomForUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var room = await roomRepository.PatialyUpdateRoomAsync(hotelId, roomId, patchDocument, ModelState);
            if (room == null)
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
