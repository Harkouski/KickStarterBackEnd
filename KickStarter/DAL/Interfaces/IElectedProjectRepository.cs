using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IElectedProjectRepository<T>
    {
        void Add(T T);

        void Delete(int id);

        List<T> DisplayAll();
        
        ArrayList DisplaySingle(string id);

    }
}
