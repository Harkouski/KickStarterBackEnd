using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Project Project { get; set; }

    }
}
