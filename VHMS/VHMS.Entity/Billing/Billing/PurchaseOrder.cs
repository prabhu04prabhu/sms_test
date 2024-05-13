using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string sPurchaseOrderDate { get; set; }

        public string Comments { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string sDeliveryDate { get; set; }
        public Supplier Supplier { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public string Status { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public Collection<PurchaseOrderTrans> PurchaseOrderTrans { get; set; }
    }

    public class PurchaseOrderTrans
    {
        public int PurchaseOrderTransID { get; set; }
        public int PurchaseOrderID { get; set; }
        public decimal Quantity { get; set; }
        public Master.Product Product { get; set; }
        public string StatusFlag { get; set; }
    }

    public class PurchaseOrderFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
}
