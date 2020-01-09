using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class ContactMaster
    {
        public int ContactId { get; set; }
        public string Dept { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string VehicleNo { get; set; }
        public string RegYear { get; set; }
        public string Model { get; set; }
        public string Message { get; set; }
        public bool IsEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
