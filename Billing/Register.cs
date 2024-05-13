using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class Register
    {
        public int RegisterID { get; set; }
        public DateTime RegisterDate { get; set; }
        public string sRegisterDate { get; set; }
        public string AccountNo { get; set; }
        public string IDProof { get; set; }
        public string IDNo { get; set; }
        public string ReceiptNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string DOB { get; set; }
        public DateTime sDuedate { get; set; }
        public string Duedate { get; set; }
        public int BranchID{ get; set; }
        public string ProofImage { get; set; }
        public string ProofBackImage { get; set; }
        public string RegisterImage1 { get; set; }
        public string RegisterImage2 { get; set; }
        public decimal BonusAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal InstallmentAmount { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public Chit Chit { get; set; }
        public User Employee { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal ChitAmount { get; set; }
        public decimal RenewalCount { get; set; }
        public string Status { get; set; }
        public User CancelledBy { get; set; }
        public DateTime CancelledDate { get; set; }
        public string sCancelledDate { get; set; }
        public string ReasonforCancel { get; set; }
    }
}
