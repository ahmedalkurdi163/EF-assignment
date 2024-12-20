using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.Json;
using XRS_API.Models;
using XRS_API.Models.ViewModels;
using XRS_API.Services;
using XRS_API.ViewModels;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace XRS_API.Controllers
{
    [Route("api/Hotels/{hotelId}/employees")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> logger;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly int maxPageSize = 10;
        public EmployeesController(ILogger<EmployeesController> logger, 
             IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.logger = logger;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees(int hotelId, int pageNumber, int pageSize, string? name)
        {
            if (pageSize > maxPageSize)
                pageSize = maxPageSize;
            var (employees, paginationMetaData) = await employeeRepository.GetEmployeesAsync(hotelId,pageNumber, pageSize, name);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(employees);
        }
        [AllowAnonymous]
        [HttpGet("{employeeId}",Name ="GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployee(int HotelId, int employeeId)
        {
            var employee = await employeeRepository.GetEmployeeAsync(HotelId, employeeId);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult<Employee?>> CreatEmployee(int hotelId,EmployeeForCreate employee)
        {
            var newemployee = await employeeRepository.CreateEmployeeAsync(hotelId, employee);
            if (newemployee == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetEmployee", new {HotelId = hotelId, employeeId = newemployee.Id }, newemployee);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int hotelId , int employeeId , EmployeeForUpdate employee)
        {
            var UpdatedEmployee = await employeeRepository.UpdateEmployeeAsync(hotelId , employeeId , employee);
            if (UpdatedEmployee == null)
                return NotFound();
            return NoContent();
        }
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int hotelId , int employeeId)
        {
            var employee = await employeeRepository.DeleteEmployeeAsync(hotelId, employeeId);
            if (employee == null)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{employeeId}")]
        public async Task<ActionResult<Employee>> PatialyUpdateEmployee(int hotelId, int employeeId, [FromBody] JsonPatchDocument<EmployeeForUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var employee = await employeeRepository.PatialyUpdateEmployeeAsync(hotelId, employeeId, patchDocument, ModelState);
            if (employee == null)
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
