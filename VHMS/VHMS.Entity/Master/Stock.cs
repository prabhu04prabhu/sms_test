using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Stock
    {
        public int StockID { get; set; }
        public DateTime StockDate { get; set; }
        public string sStockDate { get; set; }
        public Master.Product Product { get; set; }
        public Billing.Category Category { get; set; }
        public Branch Branch { get; set; }
        public string Barcode { get; set; }
        public decimal NetWeight { get; set; }
        public decimal WastagePercent { get; set; }
        public decimal Wastage { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal Ratti { get; set; }
        public decimal PureWeight { get; set; }
        public decimal Lacquer { get; set; }
        public string Making { get; set; }
        public string Design { get; set; }
        public string Karat { get; set; }
        public decimal Quantity { get; set; }
        public decimal Pieces { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string StoneName { get; set; }
        public decimal StoneWeight { get; set; }
        public Int32 StoneQuantity { get; set; }
        public decimal StoneRate { get; set; }
        public decimal StonePrice { get; set; }
        public string StoneColor { get; set; }
        public string StoneCut { get; set; }
        public string StoneClarity { get; set; }
        public decimal WholesaleMinPrice { get; set; }
        public decimal RetailMinPrice { get; set; }

        public string Status { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
