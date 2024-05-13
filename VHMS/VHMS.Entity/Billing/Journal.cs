using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Journal
    {
        public int JournalID { get; set; }
        public string JournalNo { get; set; }
        public DateTime JournalDate { get; set; }
        public string sJournalDate { get; set; }
        public string Narration { get; set; }
        public decimal Roundoff { get; set; }
        public Int16 ReceiptModeID { get; set; }
        public Ledger Bank { get; set; }
        public string ChequeNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string sIssueDate { get; set; }
        public string Status { get; set; }
        public decimal NetAmount { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public Collection<JournalTrans> JournalTrans { get; set; }
    }

    public class JournalTrans
    {
        public int JournalTransID { get; set; }
        public int JournalID { get; set; }
        public int LedgerID { get; set; }
        public string LedgerName { get; set; }
        public string Notes { get; set; }
        public string CreditOrDebit { get; set; }
        public string PartyType { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string StatusFlag { get; set; }
    }
   
}
