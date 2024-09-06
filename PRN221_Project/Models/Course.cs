using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class Course
    {
        public Course()
        {
            ProfileCourses = new HashSet<ProfileCourse>();
            Questions = new HashSet<Question>();
        }

        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? NumberQuestion { get; set; }
        public int? SubjectId { get; set; }
        public bool? Status { get; set; }
        public int? IsEnrolled { get; set; }
        public int ProfileId { get; set; }


        public virtual Subject? Subject { get; set; }
        public virtual ICollection<ProfileCourse> ProfileCourses { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
