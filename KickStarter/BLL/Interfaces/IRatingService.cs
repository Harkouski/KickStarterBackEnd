using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRatingService<T>
    {
        void Add(T T);

        void Update(T T);

        float ShowProjectRating(int projectId);
    }
}
