using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class TimeSlots
    {
        public TimeSlots()
        {
            ServiceBookingMaster = new HashSet<ServiceBookingMaster>();
        }

        public int TimeId { get; set; }
        public int CentreId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public int Capacity { get; set; }
        public int Interval { get; set; }
        public bool IsActive { get; set; }

        public virtual ServiceCentreMaster Centre { get; set; }
        public virtual ICollection<ServiceBookingMaster> ServiceBookingMaster { get; set; }
    }
}
