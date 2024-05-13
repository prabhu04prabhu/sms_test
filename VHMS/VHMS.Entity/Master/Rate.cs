using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Rate
    {
        public int RateID { get; set; }
        public DateTime RateDate { get; set; }
        public string sRateDate { get; set; }
        public Decimal Gold_22Sales { get; set; }
        public Decimal Gold_24Sales { get; set; }
        public Decimal Gold_22Purchase { get; set; }
        public Decimal Gold_24Purchase { get; set; }
        public Decimal SilverPurchase { get; set; }
        public Decimal SilverSales { get; set; }
        public Decimal Diamond_1CentPurchase { get; set; }
        public Decimal Diamond_1CentSales { get; set; }
        public Decimal Diamond_1CTPurchase { get; set; }
        public Decimal Diamond_1CTSales { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
