using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ModelImages
    {
        public int ImageId { get; set; }
        public int ModelId { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string SubContent { get; set; }
        public double? SortOrder { get; set; }

        public virtual ModelMaster Model { get; set; }
    }
}
