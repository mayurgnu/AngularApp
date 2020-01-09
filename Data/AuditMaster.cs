using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class AuditMaster
    {
        public int AuditId { get; set; }
        public string TableName { get; set; }
        public string PrimaryKeyValues { get; set; }
        public string ChangeDesc { get; set; }
        public int UserId { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual UserMaster User { get; set; }
    }
}
