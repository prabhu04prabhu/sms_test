using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class VendorPayment
    {
        public int VendorPaymentID { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string sVoucherDate { get; set; }
        public Vendor Vendor { get; set; }
        public Ledger Bank { get; set; }
        public int PaymentModeID { get; set; }
        public decimal Amount { get; set; }
        public string ChequeNo { get;set; }
        public DateTime IssueDate { get; set; }
        public string sIssueDate { get; set; }
        public DateTime CollectionDate { get; set; }
        public string sCollectionDate { get; set; }
        public string IssuedBy { get; set; }
        public string Description { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
