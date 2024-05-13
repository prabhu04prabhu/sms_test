using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Pricing
    {
        public int PricingID { get; set; }
        public Master.Product Product { get; set; }
        public string Barcode { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal MRP { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholeSalePriceA { get; set; }
        public decimal RetailPriceA { get; set; }
        public decimal WholeSalePriceB { get; set; }
        public decimal RetailPriceB { get; set; }
        public decimal WholeSalePriceC { get; set; }
        public decimal RetailPriceC { get; set; }
        public decimal RetailMargin { get; set; }
        public decimal MinDiscountPercent { get; set; }
        public decimal MaxDiscountPercent { get; set; }
        public decimal WholeSaleMargin { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
