using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
            Profiles = new HashSet<Profile>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
