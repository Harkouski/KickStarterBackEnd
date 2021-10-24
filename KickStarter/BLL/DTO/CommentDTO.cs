using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

    }
}
