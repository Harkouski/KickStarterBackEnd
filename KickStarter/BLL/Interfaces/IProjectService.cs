using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService<T>
    {
        void Add(T T);

        void Delete(int id);

        List<T> DisplayAll();

        T DisplaySingle(int id);

        List<T> GetUserProejctList(string userId);

        void UpdatePicture(IFormFile formFile, int id);

        T Update(T T);
    }
}
