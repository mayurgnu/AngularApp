using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class EventMaster
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public string FilePath { get; set; }
        public int EventType { get; set; }
        public string Photo { get; set; }
    }
}
