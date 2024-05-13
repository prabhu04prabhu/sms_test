using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class BankEntry
    {
        public int BankEntryID { get; set; }
        public DateTime EntryDate { get; set; }
        public string sEntryDate { get; set; }
        public int DebitID { get; set; }
        public int CreditID { get; set; }
        public string CreditName { get; set; }
        public string DebitName { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
