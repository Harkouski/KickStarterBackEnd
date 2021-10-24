using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserElectedProject
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { set; get; }

        public DateTime FinishDate { get; set; }

        public int CurrentDonation { get; set; }

        public int GoalDonation { get; set; }
    }
}
