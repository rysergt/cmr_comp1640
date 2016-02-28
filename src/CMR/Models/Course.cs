using System.ComponentModel.DataAnnotations;

namespace CMR.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}