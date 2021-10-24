using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRatingRepository<T>
    {
        void Add(T T);

        void Update(T T);

        float ShowProjectRating(int projectId);

    }
}
