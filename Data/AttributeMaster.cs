using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class AttributeMaster
    {
        public int AttributeId { get; set; }
        public int GroupId { get; set; }
        public string AttributeName { get; set; }
        public double? SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual AttributeGroups Group { get; set; }
    }
}
