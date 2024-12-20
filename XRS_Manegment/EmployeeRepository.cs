using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;

namespace XRS_Manegment
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public void add(Employee item)
        {
            using (var Conntext = new XRS_Context())
            {
                var Employee =  item;
                Conntext.Add(Employee);
                Conntext.SaveChanges();
            }
        }

        public void delete(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                // الاستعلام عن الموظف من قاعدة البيانات
                var employee = Conntext.Employees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    // تغيير حالته من نشط الى غير نشط
                    employee.IsActive = false;
                    Conntext.Employees.Update(employee);
                    Conntext.SaveChanges();
                }
            }
        }

        public void print(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var employee = Conntext.Employees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    Console.WriteLine($"-------Iformation for Employee his Id {id}-------\n");
                    Console.WriteLine($"Id: {employee.Id}, Name: {employee.FirstName} {employee.LastName}  \n");
                }
                else
                {
                    Console.WriteLine($"There is no Guest with this Id : {id}");
                }
            }
        }

        public void printAllItems()
        {
            using(var Conntext = new XRS_Context())
            {
                var emps = Conntext.Employees.Where(e => e.IsActive).ToList();
                foreach (var emp in emps)
                {
                    print(emp.Id);
                }
            }
        }

        public void update(int id, Employee item)
        {
            using (var Conntext = new XRS_Context())
            {
                var employee = Conntext.Employees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    employee.HotelId = item.HotelId;
                    employee.Title = item.Title;
                    employee.FirstName = item.FirstName;
                    employee.LastName = item.LastName;
                    employee.DOB = item.DOB;
                    employee.Email = item.Email;
                    employee.StratDate = item.StratDate;
                    employee.IsActive = true;

                    Conntext.Employees.Update(employee);
                    Conntext.SaveChanges();
                }
                else // Exception  في حال مالم يتم ايجاد الموظف يعطي 
                {
                    throw new Exception("Employee is Not found !");
                }
            }
        }
        

    }
}
