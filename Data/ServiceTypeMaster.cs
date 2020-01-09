using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ServiceTypeMaster
    {
        public ServiceTypeMaster()
        {
            ServiceBookingMaster = new HashSet<ServiceBookingMaster>();
        }

        public int TypeId { get; set; }
        public string ServiceType { get; set; }
        public double? SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ServiceBookingMaster> ServiceBookingMaster { get; set; }
    }
}
