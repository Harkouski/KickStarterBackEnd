using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DonationHistoryDTO
    {

        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public int Donation { get; set; }

        public DateTime Date { get; set; }
    }
}
