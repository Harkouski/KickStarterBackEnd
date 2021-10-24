using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IElectedProjectService<T>
    {
        void Add(T T);

        void Delete(int id);

        List<UserElectedProject> DisplaySingle(string id);

        List<T> DisplayAll();
    }
}
