using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Write age")]
        [Range(0, 118, ErrorMessage = "Write correct age between 0 - 118")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Write first name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Write correct name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Write second name")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Write correct last name")]
        public string LastName { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Write gender")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Write correct gender")]
        public string Gender { get; set; }


        public ICollection<Project> Projects { get; set; }

        public ICollection<DonationHistory> DonationHistories { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<ElectedProject> ElectedProjects { get; set; }

        public User()
        {
            Projects = new List<Project>();
            DonationHistories = new List<DonationHistory>();
            Comments = new List<Comment>();
            Ratings = new List<Rating>();
            ElectedProjects = new List<ElectedProject>();
        }

    }
}
