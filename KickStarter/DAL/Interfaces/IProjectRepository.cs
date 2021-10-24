using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProjectRepository<T>
    {
        void Add(T T);

        List<T> DisplayAllUserProject(string userId);

        void Delete(int id);

        List<T> DisplayAll();

        T DisplaySingle(int id);

        void UpdatePicture(IFormFile formFile, int id);

        T Update(T T);

    }
}
