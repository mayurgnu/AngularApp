using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class BannerMaster
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Photo { get; set; }
        public string BtnText { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsActive { get; set; }
        public double? SortOrder { get; set; }
        public string PreBanner { get; set; }
        public string PreTitle { get; set; }
        public string PreShortDesc { get; set; }
        public string PreBtnText { get; set; }
        public string PreRedirectUrl { get; set; }
    }
}
