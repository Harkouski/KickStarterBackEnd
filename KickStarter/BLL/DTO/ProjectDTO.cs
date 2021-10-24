using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime FinishDate { get; set; }


        public string HeaderDescription { get; set; }


        public string MainDescription { get; set; }


        public int CurrentDonation { get; set; }


        public int GoalDonation { get; set; }


        public string Picture { get; set; }


        public string UserId { get; set; }


    }
}
