using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class StockCheck
    {
        public int StockCheckID { get; set; }
        public DateTime CheckDate { get; set; }
        public string sCheckDate { get; set; }
        public string Status { get; set; }
        public User Employee { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int StockCheckNo { get; set; }
        public Collection<StockCheckTrans> StockCheckTrans { get; set; }
    }

    public class StockCheckTrans
    {
        public int StockCheckTransID { get; set; }
        public int StockCheckID { get; set; }
        public int StockID { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SMSCode { get; set; }
        public string StatusFlag { get; set; }
    }
}
