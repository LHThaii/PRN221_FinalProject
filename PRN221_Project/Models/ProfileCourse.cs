using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class ProfileCourse
    {
        public int ProfileId { get; set; }
        public int CourseId { get; set; }
        public DateTime TimeEnroll { get; set; }
        public int Process { get; set; }
        public int NumberQuestion { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Profile Profile { get; set; } = null!;
    }
}
