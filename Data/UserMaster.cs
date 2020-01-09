using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            AuditMaster = new HashSet<AuditMaster>();
            VerificationCodes = new HashSet<VerificationCodes>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual RoleMaster Role { get; set; }
        public virtual ICollection<AuditMaster> AuditMaster { get; set; }
        public virtual ICollection<VerificationCodes> VerificationCodes { get; set; }
    }
}
