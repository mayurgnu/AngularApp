using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ClassMaster
    {
        public ClassMaster()
        {
            CategoryMaster = new HashSet<CategoryMaster>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public bool IsActive { get; set; }
        public double? SortOrder { get; set; }

        public virtual ICollection<CategoryMaster> CategoryMaster { get; set; }
    }
}
