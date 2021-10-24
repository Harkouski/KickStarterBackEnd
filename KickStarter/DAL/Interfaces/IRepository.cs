using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T T);

        void Delete(int id);

        List<T> DisplayAll();

        T DisplaySingle(int id);

        T Update(T T);

    }
}
