using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using XRS_API.DbContexts;
using XRS_API.Models;
using XRS_API.Models.ViewModels;
using XRS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using System.Xml.XPath;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XRS_API.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly XRS_APPContext context;
        public EmployeeRepository(XRS_APPContext context)
        {
            this.context = context;
        }
        public async Task<(List<Employee>, PaginationMetaData)> GetEmployeesAsync(int hotelId, int pageNumber, int pageSize, string? name = null)
        {
            var employees = new List<Employee>();
            var totalItemCount = await context.Employees.CountAsync();
            var paginationdata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var quere = context.Employees as IQueryable<Employee>;
            if(!string.IsNullOrEmpty(name)) 
                quere = quere.Where(e => e.FirstName.ToLower().Contains(name) && e.HotelId == hotelId && e.IsActive);
            
            employees = await quere.Where(e => e.HotelId == hotelId && e.IsActive).OrderBy(e => e.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (employees, paginationdata);
        }

        public async Task<Employee?> GetEmployeeAsync(int HotelId, int employeeId)
        {
            return await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && e.HotelId == HotelId && e.IsActive);    
        }
        public async Task<Employee?> CreateEmployeeAsync(int hotelId, EmployeeForCreate employee)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null)
            {
                return null;
            }

            var newEmployee = new Employee()
            {
                DOB = employee.DOB,
                Email = employee.Email,
                FirstName = employee.FirstName,
                HotelId = hotelId, 
                IsActive = employee.IsActive,
                LastName = employee.LastName,
                StratDate = employee.StratDate,
                Title = employee.Title,
            };

            context.Employees.Add(newEmployee);
            await context.SaveChangesAsync(); 
            return newEmployee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(int hotelId, int employeeId, EmployeeForUpdate employee/* JsonPatchDocument<EmployeeForUpdate> PatchDocument*/)
        {
             var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
             if (hotel == null)
                 return null;
             var existingEmployee = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && e.HotelId == hotelId);
             if (existingEmployee == null)
                 return null;
             existingEmployee.Id = employeeId; 
             existingEmployee.FirstName = employee.FirstName;
             existingEmployee.LastName = employee.LastName;
             existingEmployee.StratDate = employee.StratDate;
             existingEmployee.Title = employee.Title;
             existingEmployee.IsActive = employee.IsActive;
             existingEmployee.DOB = employee.DOB;
             existingEmployee.Email = employee.Email;
             existingEmployee.HotelId = employee.HotelId;

             context.Employees.Update(existingEmployee);
             await context.SaveChangesAsync();
             return existingEmployee;
        }
        public async Task<Employee?> DeleteEmployeeAsync(int hotelId, int employeeId)
        {
            var hotel = context.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null) return null;


            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && e.HotelId == hotelId && e.IsActive);
            
            if (employee == null) return null;

            employee.IsActive = false;

            context.Employees.Update(employee);
            await context.SaveChangesAsync();
            return employee;

        }
        public async Task<Employee?> PatialyUpdateEmployeeAsync(int hotelId, int employeeId, JsonPatchDocument<EmployeeForUpdate> patchDocument, ModelStateDictionary modelState)
        {
            var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
            if (hotel == null) return null;

            var emp = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && e.HotelId == hotelId);
            if (emp == null) return null;

            var employeeToPatch = new EmployeeForUpdate()
            {
                HotelId = hotelId,
                DOB = emp.DOB,
                Email = emp.Email,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                IsActive = emp.IsActive,
                StratDate = emp.StratDate,
                Title = emp.Title,
            };
            patchDocument.ApplyTo(employeeToPatch, modelState);

            if (!modelState.IsValid)
            {
                return null;
            }
            emp.FirstName = employeeToPatch.FirstName;
            emp.LastName = employeeToPatch.LastName;
            emp.DOB = employeeToPatch.DOB;
            emp.Email = employeeToPatch.Email;
            emp.IsActive = employeeToPatch.IsActive;
            emp.StratDate = employeeToPatch.StratDate;
            emp.Title = employeeToPatch.Title;

            context.Employees.Update(emp);
            await context.SaveChangesAsync();
            return emp;
        }
    }
}
