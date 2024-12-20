using XRS_API.Models.ViewModels;
using XRS_API.Models;
using XRS_API.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XRS_API.Services
{
    public interface IEmployeeRepository
    {
        Task<(List<Employee>, PaginationMetaData)> GetEmployeesAsync(int hotelId,int pageNumber, int pageSize, string? name);
        Task<Employee?> GetEmployeeAsync(int HotelId , int employeeId);

        Task<Employee?> CreateEmployeeAsync(int HotelId, EmployeeForCreate employee);
        Task<Employee?> UpdateEmployeeAsync(int hotelId,int employeeId,EmployeeForUpdate employee);
        Task<Employee?> PatialyUpdateEmployeeAsync(int hotelId, int employeeId, JsonPatchDocument<EmployeeForUpdate> employee, ModelStateDictionary modelState);

        Task<Employee?> DeleteEmployeeAsync(int hotelId,int employeeId);
    }
}
