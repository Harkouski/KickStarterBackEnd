using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Project
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

        public User User { get; set; }

        public string UserId { get; set; }


        public ICollection<DonationHistory> DonationHistories { get; set; }


        public ICollection<Comment> Comments { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<ElectedProject> ElectedProjects { get; set; }


        public Project()
        {
            DonationHistories = new List<DonationHistory>();
            Comments = new List<Comment>();
            Categories = new List<Category>();
            Ratings = new List<Rating>();
            ElectedProjects = new List<ElectedProject>();
        }

    }
}
