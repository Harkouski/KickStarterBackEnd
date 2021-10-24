using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ProjectCommentsModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public int ProjectId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

    }
}
