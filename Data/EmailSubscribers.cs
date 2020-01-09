using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class EmailSubscribers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
