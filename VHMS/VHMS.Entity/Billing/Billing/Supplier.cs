using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierAddress { get; set; }
        public State State { get; set; }        
        public string City { get; set; }
        public string Taluk { get; set; }
        public string Pincode { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string LandLine { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public DateTime RateUpdatedDate { get; set; }
        public string sRateUpdatedDate { get; set; }
        public int IsRateUpdated { get; set; }
        public string Area { get; set; }
        public string WebSite { get; set; }
        public string ImagePath { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean LinkClick { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountHolderName { get; set; }
        public string IFSCCode { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int Days { get; set; }
    }
}
