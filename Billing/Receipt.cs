using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public string VoucherNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public string sVoucherDate { get; set; }
        public Customer Customer { get; set; }
        public Ledger Bank { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public Int16 ReceiptModeID { get; set; }
        public string SalesEntryIDs { get; set; }
        public decimal Amount { get; set; }
        public decimal TDSPayment { get; set; }
        public decimal OnAccount { get; set; }
        public string ChequeNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string sIssueDate { get; set; }
        public DateTime CollectionDate { get; set; }
        public string sCollectionDate { get; set; }
        public string IssuedBy { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string BankName { get; set; }
        public Boolean IsRetail { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Charges { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DocumentPath { get; set; }
        public string InvoiceNos { get; set; }
     

    }
}
