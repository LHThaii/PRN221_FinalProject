using System;
using System.Collections.Generic;

namespace PRN221_Project.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string? Hash { get; set; }
        public string? ConfirmationToken { get; set; }
        public string? ResetPaswordToken { get; set; }
        public bool? Confirmed { get; set; }
        public bool? Blocked { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; } = null!;
        public bool? IsActivated { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}
