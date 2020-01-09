using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class CategoryMaster
    {
        public CategoryMaster()
        {
            ModelMaster = new HashSet<ModelMaster>();
        }

        public int CategoryId { get; set; }
        public int ClassId { get; set; }
        public string Category { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
        public double? SortOrder { get; set; }

        public virtual ClassMaster Class { get; set; }
        public virtual ICollection<ModelMaster> ModelMaster { get; set; }
    }
}
