using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class AttributeGroups
    {
        public AttributeGroups()
        {
            AttributeMaster = new HashSet<AttributeMaster>();
            ModelAttributes = new HashSet<ModelAttributes>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public double? SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AttributeMaster> AttributeMaster { get; set; }
        public virtual ICollection<ModelAttributes> ModelAttributes { get; set; }
    }
}
