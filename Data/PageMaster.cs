using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class PageMaster
    {
        public PageMaster()
        {
            PermissionMaster = new HashSet<PermissionMaster>();
        }

        public int PageId { get; set; }
        public int ModuleId { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public bool IsShowMenu { get; set; }
        public bool IsActive { get; set; }
        public bool? IsShowSearch { get; set; }
        public string TableNames { get; set; }

        public virtual ModuleMaster Module { get; set; }
        public virtual ICollection<PermissionMaster> PermissionMaster { get; set; }
    }
}
