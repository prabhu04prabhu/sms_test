using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class ExpensePayment
    {
        public int ExpensePaymentID { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string sVoucherDate { get; set; }
        public Boolean BillType { get; set; }
        public string PurchaseIDs { get; set; }
        public Ledger Supplier { get; set; }
        public Ledger Bank { get; set; }
        public int PaymentModeID { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public string ChequeNo { get;set; }
        public string BillNos { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
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
        public decimal Charges { get; set; }
        public decimal OtherDiscount { get; set; }
        public string PurchaseNo { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DocumentPath { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public Boolean IsAdvance { get; set; }
    }
}
