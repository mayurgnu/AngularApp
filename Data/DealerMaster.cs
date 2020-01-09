using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class DealerMaster
    {
        public DealerMaster()
        {
            WarrantyMaster = new HashSet<WarrantyMaster>();
        }

        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public double? SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<WarrantyMaster> WarrantyMaster { get; set; }
    }
}
