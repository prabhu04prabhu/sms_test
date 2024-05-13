using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class SalesDiscount
    {
        public int SalesDiscountID { get; set; }
        public string DiscountNo { get; set; }
        public DateTime DiscountDate { get; set; }
        public string sDiscountDate { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
        public Customer Customer { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Roundoff { get; set; }
        public SalesEntry SalesEntry { get; set; }
        public SalesEntry AdjSales { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ImagePath { get; set; }
        public string Comments { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public string Notes { get; set; }
        public DateTime SalesDate { get; set; }
        public string sSalesDate { get; set; }
    }

}
