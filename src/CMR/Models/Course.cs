using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMR.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        
        public virtual ICollection<Assignment> Managers { get; set; }
        public virtual ApplicationUser Creator { get; set; }
    }
}