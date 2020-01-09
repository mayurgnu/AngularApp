using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ModuleMaster
    {
        public ModuleMaster()
        {
            PageMaster = new HashSet<PageMaster>();
        }

        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PageMaster> PageMaster { get; set; }
    }
}
