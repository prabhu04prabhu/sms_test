using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Sales
    {
        public int SalesID { get; set; }
        public string InvoiceNo { get; set; }
        public string AuditBillNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string sInvoiceDate { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal ExchangeAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string CardNo { get; set; }
        public User CreatedBy { get; set; }
        public User Employee { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Boolean IsActive { get; set; }       
        public Collection<SalesTrans> SalesTrans { get; set; }
        public Collection<SalesChitTrans> SalesChitTrans { get; set; }
        public Collection<ExchangeTrans> ExchangeTrans { get; set; }
        public Register Register { get; set; }
        public decimal CardAmount { get; set; }
        public decimal ReturnAmount { get; set; }

        public decimal Exchange { get; set; }
        public decimal Roundoff { get; set; }
        public User SalesMan { get; set; }
    }

    public class SalesTrans
    {
        public int SalesTransID { get; set; }
        public int SalesID { get; set; }
        public int StockID { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Subtotal { get; set; }
        public Stock Stock { get; set; }
        public string StatusFlag { get; set; }      
        public DateTime StockDate { get; set; }
        public string sStockDate { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public decimal NetWeight { get; set; }
        public string Barcode { get; set; }
        public decimal WastagePercent { get; set; }
        public decimal Wastage { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal Ratti { get; set; }
        public decimal PureWeight { get; set; }
        public decimal Lacquer { get; set; }
        public string Making { get; set; }
        public string Design { get; set; }
        public string Karat { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string StoneName { get; set; }
        public decimal StoneWeight { get; set; }
        public int StoneQuantity { get; set; }
        public decimal StoneRate { get; set; }
        public decimal StonePrice { get; set; }
        public string StoneColor { get; set; }
        public string StoneCut { get; set; }
        public string StoneClarity { get; set; }
        public int BranchID { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }

    public class SalesChitTrans
    {
        public int SalesChitTransID { get; set; }
        public int SalesID { get; set; }
        public Register Register { get; set; }
        public decimal ChitAmount { get; set; }
        public string StatusFlag { get; set; }
    }

    public class SalesFilter
    {
        public int CustomerID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int BranchID { get; set; }
        public int SalesID { get; set; }
        public int UserID { get; set; }
       // public int SalesManID { get; set; }
    }
}
