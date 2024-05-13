using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class StockAdjuest
    {
        public int StockAdjustID { get; set; }
        public Master.Product Product { get; set; }
        public string Barcode { get; set; }
        public decimal Available_Qty { get; set; }
        public decimal Updated_Qty { get; set; }
        public decimal Purchase_Price { get; set; }
        public decimal RetailSalesMargin { get; set; }
        public decimal WholeSalesMargin { get; set; }
        public decimal RetailSalesPrice { get; set; }
        public decimal WholeSalesPrice { get; set; }
        public string AdjustType { get; set; }        
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string sCreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
