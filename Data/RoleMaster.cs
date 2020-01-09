using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            PermissionMaster = new HashSet<PermissionMaster>();
            UserMaster = new HashSet<UserMaster>();
        }

        public int RoleId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PermissionMaster> PermissionMaster { get; set; }
        public virtual ICollection<UserMaster> UserMaster { get; set; }
    }
}
