using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XRS_Data;
using XRS_Domain;

namespace XRS_Manegment
{
    public class PaymentRepository : IRepository<Payment>
    {
        public void add(Payment item)
        {
            using (var Conntext = new XRS_Context())
            {
                var payment = item;
                Conntext.Add(payment);
                Conntext.SaveChanges();
            }
        }

        public void delete(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                var payment = Conntext.Payments.FirstOrDefault(e => e.Id == id && e.IsActive == true);
                if (payment != null)
                {
                    payment.IsActive = false;
                    Conntext.Payments.Update(payment);
                    Conntext.SaveChanges();
                }
            }
        }

        public void print(int id)
        {
            using (var Conntext = new XRS_Context())
            {
                Console.WriteLine($"-----Payment {id} Information-----");
                // الاستعلام عن معلومات الدفع الخاصة بالزائر
                var payment = Conntext.Payments.FirstOrDefault(b => b.GuestId == id && b.IsActive == true);
                if (payment != null)
                {
                    Console.WriteLine($"-------Iformation for Payment his Id {id}-------\n");

                                                                 // من اجل عرض التاريخ فقط دون الوقت 
                    Console.WriteLine($"Id: {payment.Id}, Create Date: {payment.CreateDate.ToString("dd/MM/yyyy")}, Toutal Amount: {payment.ToutalAmount}$\n");
                }
                else
                {
                    Console.WriteLine($"There is no Payment with this Id : {id}");
                }
            }
        }
        public void printAllItems()
        {
            using (var Conntext = new XRS_Context())
            {
                var payments = Conntext.Payments.Where(h => h.IsActive).ToList();
                foreach (var payment in payments)
                {
                    print(payment.Id);
                }
            }
        }
        public void update(int id, Payment item)
        {
            using (var Conntext = new XRS_Context())
            {
                var payment = Conntext.Payments.FirstOrDefault(p => p.Id == id);
                if (payment != null)
                {
                    payment.IsActive = true;
                    payment.CreateDate = item.CreateDate;
                    payment.ToutalAmount = item.ToutalAmount;
                    payment.BookingId = item.BookingId;
                    payment.GuestId = item.GuestId;

                    Conntext.Payments.Update(payment);
                    Conntext.SaveChanges();
                }
                else // Exception  في حال مالم يتم ايجاد الدفعة  يعطي 
                {
                    throw new Exception("Payment is Not found !");
                }
            }
        }
    }
}
