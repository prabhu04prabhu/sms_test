using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class CashTill
    {
        public int CashTillID { get; set; }
        public DateTime TillDate { get; set; }
        public string sTillDate { get; set; }
        public int OneRs { get; set; }
        public int TwoRs { get; set; }
        public int FiveRs { get; set; }
        public int TenRs { get; set; }
        public int TwentyRs { get; set; }
        public int FiftyRs { get; set; }
        public int HundredRs { get; set; }
        public int TwoHundredRs { get; set; }
        public int FiveHundredRs { get; set; }
        public int ThousandRs { get; set; }
        public int TwoThousandRs { get; set; }
        public Decimal CardAmount { get; set; }
        public Decimal TotalAmount { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Branch Branch { get; set; }
        public string Notes { get; set; }

    }
}
