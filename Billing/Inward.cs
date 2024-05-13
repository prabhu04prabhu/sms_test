using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Inward
    {
        public int InwardID { get; set; }
        public string InwardNo { get; set; }
        public DateTime InwardDate { get; set; }
        public string sInwardDate { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalQuantity { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public string CEmployeeName { get; set; }
        public string MEmployeeName { get; set; }
        public string Comments { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public Collection<InwardTrans> InwardTrans { get; set; }
        public string SupplierName { get; set; }
    }

    public class InwardTrans
    {
        public int InwardTransID { get; set; }
        public int InwardID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public string Barcode { get; set; }
        public decimal SubTotal { get; set; }
        public Master.Product Product { get; set; }
        public string StatusFlag { get; set; }
    }

    public class InwardFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
}
