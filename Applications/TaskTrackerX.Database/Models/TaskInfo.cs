using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerX.Database.Models
{

    public class TaskInfo
    {
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public Guid AssignedToUserId { get; set; }
        public string Status { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateExpiration { get; set; }
    }
}
