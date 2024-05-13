using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Billing
{
    public class Agent
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgentCode { get; set; }
        public string AgentAddress { get; set; }
        public State State { get; set; }        
        public string City { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string WhatsAppNo { get; set; }
        public string EmailID { get; set; }
        public Boolean IsActive { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNo { get; set; }
        public string AccountHolderName { get; set; }
        public string CommissionType { get; set; }
        public string CommissionPercentage { get; set; }
        public string CommissionAmount { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
