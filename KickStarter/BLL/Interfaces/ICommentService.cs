using BLL.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService<T>
    {
        void Add(T T);

        void Delete(int id);

        List<T> DisplayAll();

        List<ProjectCommentsModel> DisplayAllProjectComments(int id);

        T Update(T T);
    }
}
