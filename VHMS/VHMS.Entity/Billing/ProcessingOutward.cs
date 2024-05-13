using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class ProcessingOutward
    {
        public int ProcessingOutwardID { get; set; }
        public string ProcessingOutwardNo { get; set; }
        public DateTime ProcessingOutwardDate { get; set; }
        public string sProcessingOutwardDate { get; set; }
        public Vendor Vendor { get; set; }
        public Work Work { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public Collection<ProcessingOutwardTrans> ProcessingOutwardTrans { get; set; }
    }

    public class ProcessingOutwardTrans
    {
        public int ProcessingOutwardTransID { get; set; }
        public int ProcessingOutwardID { get; set; }
        public int ProcessingInwardTransID { get; set; }
        public string ProcessingInwardNo { get; set; }
        public DateTime ProcessingInwardDate { get; set; }
        public string sProcessingInwardDate { get; set; }
        
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal SubTotal { get; set; }
        public Master.Product Product { get; set; }
        public string StatusFlag { get; set; }
    }

    public class ProcessingOutwardFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
}
