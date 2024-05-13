using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class PurchaseDiscount
    {
        public int PurchaseDiscountID { get; set; }
        public string DiscountNo { get; set; }
        public DateTime DiscountDate { get; set; }
        public string sDiscountDate { get; set; }
        public Supplier Supplier { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Roundoff { get; set; }
        public Purchase Purchase { get; set; }
        public Purchase AdjPurchase { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ImagePath { get; set; }
        public string Notes { get; set; }
        public FinancialYear FinancialYear { get; set; }
    }

}
