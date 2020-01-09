using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class PermissionMaster
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public int PageId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsView { get; set; }

        public virtual PageMaster Page { get; set; }
        public virtual RoleMaster Role { get; set; }
    }
}
