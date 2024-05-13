using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Exchange
    {
        public int ExchangeID { get; set; }
        public string ExchangeNo { get; set; }
        public DateTime ExchangeDate { get; set; }
        public string sExchangeDate { get; set; }
        public string InvoiceNo { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public Tax Tax { get; set; }
        public int SalesID { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }       
        public decimal NetAmount { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsActive { get; set; }       
        public Collection<ExchangeTrans> ExchangeTrans { get; set; }
    }

    public class ExchangeTrans
    {
        public int ExchangeTransID { get; set; }
        public int ExchangeID { get; set; }
        public Category Category { get; set; }
        public Master.Product Product { get; set; }
        public decimal NetWeight { get; set; }
        public decimal MeltingWeight { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal StoneWeight { get; set; }
        public string Karat { get; set; }
        public decimal CurrentRate { get; set; }
        public decimal Amount { get; set; }
        public int SalesID { get; set; }
        public string StatusFlag { get; set; }
    }

    public class ExchangeFilter
    {
        public int CustomerID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int BranchID { get; set; }
        public int ExchangeID { get; set; }
        public int UserID { get; set; }       
    }
}
