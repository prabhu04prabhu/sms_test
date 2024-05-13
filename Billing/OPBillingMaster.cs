using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class OPBillingMaster
    {
        public int OPID { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
        public patient Patient { get; set; }
        public Discharge.Doctor Doctor { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string DescriptionName { get; set; }

        public Boolean IsCancelled { get; set; }
        public string CancelReason { get; set; }
        public decimal Subtotal { get; set; }
        public string CUserName { get; set; }
        public Collection<OPBillingTrans> OPBillingTrans { get; set; }
    }

    public class OPBillingTrans
    {
        public int OPTransID { get; set; }
        public int OPID { get; set; }
        public int DescriptionID { get; set; }
        //public int InstructionType { get; set; }
        public string DescriptionName { get; set; }
        public decimal Charges { get; set; }
        public decimal Subtotal { get; set; }
        public Description Description { get; set; }
        public string StatusFlag { get; set; }
    }

    public class OPBillingFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int DescriptionID { get; set; }
        public int UserID { get; set; }
    }
}
