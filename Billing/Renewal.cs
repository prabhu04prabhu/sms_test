using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class Renewal
    {
        public int RenewalID { get; set; }
        public DateTime RenewalDate { get; set; }
        public string sRenewalDate { get; set; }
        public string RenewalNo { get; set; }
        public string Status { get; set; }
        public decimal AmountPaid { get; set; }
        public Register Register { get; set; }        
        public Branch Branch { get; set; }
        public User REmployee { get; set; }
        public User Employee { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean isRegisterEntry { get; set; }
    }
}
