using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class Profile
    {
        public Profile()
        {
            ProfileCourses = new HashSet<ProfileCourse>();
            Subjects = new HashSet<Subject>();
        }

        public int ProfileId { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public bool? Blocked { get; set; }
        public int RoleId { get; set; }
        public bool? Status { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<ProfileCourse> ProfileCourses { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
