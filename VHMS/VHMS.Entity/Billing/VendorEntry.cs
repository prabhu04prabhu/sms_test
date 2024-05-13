using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class VendorEntry
    {
        public int VendorEntryID { get; set; }
        public string Status { get; set; }
        public DateTime ClosingDate { get; set; }
        public string sClosingDate { get; set; }

        public string Comments { get; set; }
        public Vendor Vendor { get; set; }
        public int TotalInQty { get; set; }
        public int TotalOutQty { get; set; }
        public int BalanceQty { get; set; }
        public int OpeningQty { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string MonthYear { get; set; }
        public Collection<VendorTrans> VendorTrans { get; set; }
        public Collection<VendorPaymentMode> VendorPaymentMode { get; set; }
    }

    public class VendorTrans
    {
        public int VendorTransID { get; set; }
        public int VendorEntryID { get; set; }
        public DateTime EntryDate { get; set; }
        public string sEntryDate { get; set; }
        public string EntryType { get; set; }
        public int InQty { get; set; }
        public int RePolishQty { get; set; }
        public int OutQty { get; set; }
        public int ReturnQty { get; set; }
        public int Balance_Qty { get; set; }
        public decimal DamageAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public string StatusFlag { get; set; }
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int BankID { get; set; }
        public string LedgerName { get; set; }
        public int PaymentModeID { get; set; }
        public string ChequeNo { get; set; }
        public string PaymentMode { get; set; }
        public DateTime CollectionDate { get; set; }
        public string sCollectionDate { get; set; }
        public int RePolish { get; set; }
        public decimal TDSPercent { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal PayableAmount { get; set; }
    }
    public class VendorPaymentMode
    {
        public int VendorPaymentModeID { get; set; }
        public int VendorEntryID { get; set; }
        public int BankID { get; set; }
        public string PaymentMode { get; set; }
        public SalesEntry SalesEntry { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public decimal Charges { get; set; }
        public string IssuedBy { get; set; }
        public string Status { get; set; }
        public DateTime IssueDate { get; set; }
        public string sIssueDate { get; set; }
        public DateTime CollectionDate { get; set; }
        public string sCollectionDate { get; set; }
        public string StatusFlag { get; set; }
    }

    public class VendorStockCheck
    {
        public int Quantity { get; set; }
        public int RePolishQuantity { get; set; }


    }


}
