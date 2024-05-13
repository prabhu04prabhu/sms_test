using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Ledger
    {
        public int LedgerID { get; set; }
        public string LedgerName { get; set; }
        public decimal OpeningBalance { get; set; }
        public string OpeningBalanceType { get; set; }
        public LedgerType LedgerType { get; set; }
        public Boolean IsDefaultRecord { get; set; }
        public Boolean IsActive { get; set; }
        public State State { get; set; }
        public string City { get; set; }
        public Boolean AddDetail { get; set; }
        public string PhoneNo { get; set; }
        public string GSTIN { get; set; }
        public string Address { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
