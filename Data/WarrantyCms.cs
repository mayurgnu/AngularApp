using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class WarrantyCms
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
    }
}
