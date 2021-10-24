using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DonationHistory
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public int Donation { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Project Project { get; set; }

    }
}
