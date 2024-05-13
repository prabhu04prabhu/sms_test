using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public string VendorAddress { get; set; }

        public string Comments { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }

        public string PANNo { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Collection<VendorWorkTrans> VendorWorkTrans { get; set; }
    }

    public class VendorWorkTrans
    {
        public int VendorWorkTransID { get; set; }
        public int VendorID { get; set; }
        public int WorkID { get; set; }
        public decimal Amount { get; set; }
        public string StatusFlag { get; set; }
        public string WorkName { get; set; }
    }
}
