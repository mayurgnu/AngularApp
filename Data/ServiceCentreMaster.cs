using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ServiceCentreMaster
    {
        public ServiceCentreMaster()
        {
            ServiceBookingMaster = new HashSet<ServiceBookingMaster>();
            TimeSlots = new HashSet<TimeSlots>();
        }

        public int CentreId { get; set; }
        public string CentreName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BlockDays { get; set; }
        public double? SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ServiceBookingMaster> ServiceBookingMaster { get; set; }
        public virtual ICollection<TimeSlots> TimeSlots { get; set; }
    }
}
