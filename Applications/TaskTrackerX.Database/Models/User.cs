using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerX.Database.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        //Navigation prop
        public virtual Role? Role { get; set; }

        public virtual ICollection<TaskInfo>? CreatedTask { get; set; }
        public virtual ICollection<TaskInfo>? AssignedTask { get; set; }
    }
}
