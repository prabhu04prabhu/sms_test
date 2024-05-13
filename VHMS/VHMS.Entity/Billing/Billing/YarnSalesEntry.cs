using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class YarnSalesEntry
    {
        public int YarnSalesEntryID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string sInvoiceDate { get; set; }
        public string sWholeSalesInvoiceDate { get; set; }
        public Customer Customer { get; set; }
        public CustomerTypes CustomerType { get; set; }
        public State State { get; set; }
        public Gift Gift { get; set; }
        public Agent Agent { get; set; }
        public Tax Tax { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdditionalDiscount { get; set; }
        public decimal Roundoff { get; set; }
        public decimal NetAmount { get; set; }
        public string PaymentMode { get; set; }
        public Ledger Bank { get; set; }
        public SalesOrder SalesOrder { get; set; }
        public Transport Transport { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string Narration { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsRetailBill { get; set; }
        public string CancelReason { get; set; }
        public decimal TotalQty { get; set; }
        public string Status { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal TDSPayment { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string sCreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public decimal SalesPoints { get; set; }
        public decimal TenderAmount { get; set; }
        public decimal BalanceGiven { get; set; }
        public DateTime LRDate { get; set; }
        public string sLRDate { get; set; }
        public string CardNo { get; set; }
        public string VehicleNo { get; set; }
        public string EWayNo { get; set; }
        public string LRNo { get; set; }
        public string TransportName { get; set; }
        public decimal CardCharges { get; set; }
        public decimal ExchangeAmount { get; set; }
        public decimal UsedPoints { get; set; }
        public decimal TCSPercent { get; set; }
        public decimal TCSAmount { get; set; }
        public string NoofBags { get; set; }
        public string Notes { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        //public int Days { get; set; }
        public int DueDays { get; set; }
        public string BillStatus { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string sDeliveryDate { get; set; }
        public string RetailsPaymentMode { get; set; }
        public decimal TransportCharges { get; set; }
        public decimal OtherCharges { get; set; }
        public Collection<YarnSalesEntryTrans> YarnSalesEntryTrans { get; set; }
        public Collection<SalesExchangeTrans> SalesExchangeTrans { get; set; }
        public Collection<RetailPaymentMode> RetailPaymentMode { get; set; }
    }

    public class YarnSalesEntryTrans
    {
        public int YarnSalesEntryTransID { get; set; }
        public int YarnSalesEntryID { get; set; }
        public decimal Quantity { get; set; }
        public string Barcode { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal { get; set; }
        public Master.Product Product { get; set; }
        public string StatusFlag { get; set; }
        public Tax Tax { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        
        public decimal TaxAmount { get; set; }

        public Boolean NewProductFlag { get; set; }
    }
    
    public class YarnSalesEntryFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
    
}
