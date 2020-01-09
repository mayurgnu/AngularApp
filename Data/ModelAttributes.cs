using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ModelAttributes
    {
        public int PrAttrId { get; set; }
        public string Attribute { get; set; }
        public string AttrValue { get; set; }
        public string AttrGroup { get; set; }
        public int ModelId { get; set; }
        public int? GroupId { get; set; }
        public double? SortOrder { get; set; }

        public virtual AttributeGroups Group { get; set; }
        public virtual ModelMaster Model { get; set; }
    }
}
