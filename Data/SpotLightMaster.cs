using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class SpotLightMaster
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string BtnText { get; set; }
        public string LnkName { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsSpotLight { get; set; }
    }
}
