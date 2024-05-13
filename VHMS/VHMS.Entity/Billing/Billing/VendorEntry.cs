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
        public Collection<VendorTrans> VendorTrans { get; set; }
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
        public DateTime CollectionDate { get; set; }
        public string sCollectionDate { get; set; }

    }

}
