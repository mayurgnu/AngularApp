using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ModelMaster
    {
        public ModelMaster()
        {
            ModelAttributes = new HashSet<ModelAttributes>();
            ModelImages = new HashSet<ModelImages>();
            ServiceBookingMaster = new HashSet<ServiceBookingMaster>();
            WarrantyMaster = new HashSet<WarrantyMaster>();
        }

        public int ModelId { get; set; }
        public int CategoryId { get; set; }
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        public string ModelImg { get; set; }
        public string Tag { get; set; }
        public string Sec1Title { get; set; }
        public string Sec1Img { get; set; }
        public string Sec2Title { get; set; }
        public string Sec2Content { get; set; }
        public string Sec2LeftImg1 { get; set; }
        public string Sec2LeftImg2 { get; set; }
        public string Sec2RightImg1 { get; set; }
        public string Sec2RightImg2 { get; set; }
        public string Sec3Content { get; set; }
        public string Sec3Img { get; set; }
        public string Sec3VideoUrl { get; set; }
        public DateTime PublishOn { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public double? SortOrder { get; set; }

        public virtual CategoryMaster Category { get; set; }
        public virtual ICollection<ModelAttributes> ModelAttributes { get; set; }
        public virtual ICollection<ModelImages> ModelImages { get; set; }
        public virtual ICollection<ServiceBookingMaster> ServiceBookingMaster { get; set; }
        public virtual ICollection<WarrantyMaster> WarrantyMaster { get; set; }
    }
}
