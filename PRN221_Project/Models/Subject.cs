using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
        }

        public int SubjectId { get; set; }
        public string Name { get; set; } = null!;
        public bool? Status { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }
    }
}
