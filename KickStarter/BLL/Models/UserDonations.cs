using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserDonations
    {
        public int Id { get; set; }

        public string ProjectName { set; get; }

        public DateTime Date { get; set; }

        public int Donation { get; set; }
    }
}
