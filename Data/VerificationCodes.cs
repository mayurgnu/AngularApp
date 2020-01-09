using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class VerificationCodes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsUsed { get; set; }

        public virtual UserMaster User { get; set; }
    }
}
