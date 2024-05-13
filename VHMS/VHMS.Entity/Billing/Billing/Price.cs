using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Price
    {
        public decimal Rate { get; set; }
        public string SMSCode { get; set; }
        public string ProductCode { get; set; }
        public decimal RetailRate { get; set; }
        public decimal WholeSalePriceA { get; set; }
        public decimal WholeSalePriceB   { get; set; }
        public decimal WholeSalePriceC { get; set; }
        public decimal RetailPriceA { get; set; }
        public decimal RetailPriceB { get; set; }
        public decimal RetailPriceC { get; set; }
        public string Type { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
