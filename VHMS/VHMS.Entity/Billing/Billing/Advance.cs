using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Advance
    {
        public int AdvanceID { get; set; }
        public DateTime DateofGiven { get; set; }
        public string sDateofGiven { get; set; }
        public Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public string AdvanceType { get; set; }
        public string Comments { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
