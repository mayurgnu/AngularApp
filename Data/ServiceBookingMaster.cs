using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ServiceBookingMaster
    {
        public int ServiceId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int? ModelId { get; set; }
        public string VehModelNo { get; set; }
        public string VehRegNo { get; set; }
        public int CentreId { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public string Remark { get; set; }
        public DateTime ServiceDate { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }

        public virtual ServiceCentreMaster Centre { get; set; }
        public virtual ModelMaster Model { get; set; }
        public virtual ServiceTypeMaster ServiceTypeNavigation { get; set; }
        public virtual TimeSlots TimeSlot { get; set; }
    }
}
