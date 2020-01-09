using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class UserLoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public DateTime OnDate { get; set; }
    }
}
