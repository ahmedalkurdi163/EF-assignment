using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRS_Manegment
{
    public interface IRepository<T>
    {
        void add(T item);
        void update(int id,T item);
        void print(int id);
        void delete(int id);
        void printAllItems();
    }
}
