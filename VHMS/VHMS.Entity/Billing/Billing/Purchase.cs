using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string sPurchaseDate { get; set; }
        public string sBillDate { get; set; }
        public Supplier Supplier { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal PaymentDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TCSAmount { get; set; }
        public decimal TCSPercent { get; set; }
        public decimal Roundoff { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal PurchaseDiscount { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal CourierCharges { get; set; }
        public string DocumentPath { get; set; }
        public decimal PaymentDiscountPercent { get; set; }
        public string Dis_amt_Type { get; set; }
        public string VerifiedName { get; set; }
        public string ConfirmedName { get; set; }
        public int VerifiedBy { get; set; }
        public int ConfirmedBy { get; set; }
        public decimal TotalQty { get; set; }
        public int DCID { get; set; }
        public int DueDays { get; set; }
        public Boolean BillType { get; set; }
        public Boolean IsDC { get; set; }
        public Boolean IsDCConverted { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public string MUserName { get; set; }
        public string CEmployeeName { get; set; }
        public string MEmployeeName { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public Boolean IsYarn { get; set; }
        public Collection<PurchaseTrans> PurchaseTrans { get; set; }
    }

    public class PurchaseTrans
    {
        public int PurchaseTransID { get; set; }
        public int PurchaseID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public string Barcode { get; set; }
        public decimal SubTotal { get; set; }
        public Tax Tax { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public Master.Product Product { get; set; }
        public string StatusFlag { get; set; }
        public Boolean RateupdateFlag { get; set; }
        public Boolean NewProductFlag { get; set; }
        public Boolean RateDecreaseFlag { get; set; }

    }

    public class PurchaseFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }


    public class PurchaseTransDetails
    {
        public int PurchaseTransID { get; set; }
        public int PurchaseID { get; set; }
        public decimal Quantity { get; set; }
        public string Barcode { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal { get; set; }
        public Master.Product Product { get; set; }
        public string SupplierName { get; set; }
        public string StatusFlag { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string sPurchaseDate { get; set; }
    }
}
