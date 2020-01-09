using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class WarrantyMaster
    {
        public int WarrantyId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int ModelId { get; set; }
        public string ModelNo { get; set; }
        public string ModelName { get; set; }
        public string VehRegNo { get; set; }
        public DateTime VehRegDate { get; set; }
        public string VehChassisNo { get; set; }
        public string VehFrameNo { get; set; }
        public int? DealerId { get; set; }
        public string DealerName { get; set; }
        public string PurchaseProof { get; set; }
        public string Newsletter { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual DealerMaster Dealer { get; set; }
        public virtual ModelMaster Model { get; set; }
    }
}
