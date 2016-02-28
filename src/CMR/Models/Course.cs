using System.ComponentModel.DataAnnotations;

namespace CMR.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}