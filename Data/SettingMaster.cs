using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class SettingMaster
    {
        public int SettingId { get; set; }
        public string TagCode { get; set; }
        public string TagName { get; set; }
        public string TagValue { get; set; }
        public string RelatedValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; }
    }
}
