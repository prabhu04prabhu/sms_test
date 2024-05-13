using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class TDSPayment
    {
        public int TDSPaymentID { get; set; }
        public string TDSPaymentNo { get; set; }
        public DateTime TDSPaymentDate { get; set; }
        public string sTDSPaymentDate { get; set; }
        public string Narration { get; set; }
        public Customer Customer { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string SlipNo { get; set; }
        public DateTime SlipDate { get; set; }
        public string sSlipDate { get; set; }
        public string DocumentPath { get; set; }
        public decimal Roundoff { get; set; }
        public int ManualPayment { get; set; }
        public decimal TotalAmount { get; set; }
        public Collection<TDSPaymentTrans> TDSPaymentTrans { get; set; }
    }

    public class TDSPaymentTrans
    {
        public int TDSPaymentTransID { get; set; }
        public int TDSPaymentID { get; set; }
        public SalesEntry SalesEntry { get; set; }
        public SalesEntry oldSalesEntry { get; set; }
        public decimal TDSPercent { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal Roundoff { get; set; }
        public string StatusFlag { get; set; }
    }
}
