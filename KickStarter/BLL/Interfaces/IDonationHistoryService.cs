using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDonationHistoryService<T>
    {
        void Add(T T);

        void Delete(int id);

        List<T> DisplayAll();

        List<UserDonations> DisplaySingle(string id);

        T Update(T T);
    }
}
