using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMR.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public virtual ApplicationUser Manager { get; set; }
        public DateTime AssignDate { get; set; }
    }
}