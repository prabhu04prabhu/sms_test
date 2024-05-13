﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class INLREntry
    {
        public int INLREntryID { get; set; }
        public string LREntryNo { get; set; }
        public DateTime LREntryDate { get; set; }
        public string sLREntryDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string sDeliveryDate { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public string FollowedPerson { get; set; }
        public Supplier Supplier { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string Description { get; set; }
        public decimal NetAmount { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public decimal FrieghtCharges { get; set; }
        public decimal TotalQty { get; set; }
        public string TransportBranch { get; set; }
        public string TemplateMessage { get; set; }        
        public Transport Transport { get; set; }
        public string Status { get; set; }
        public string Payment { get; set; }
        public string EWayNo { get; set; }
        public int PurchaseID { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string sPurchaseDate { get; set; }
        public string AWBNo { get; set; }
        public string NoofBags { get; set; }
        public string DocumentPath1 { get; set; }
        public string DocumentPath { get; set; }
        public Collection<INLREntryTrans> INLREntryTrans { get; set; }
    }

    public class INLREntryTrans
    {
        public int INLREntryTransID { get; set; }
        public int INLREntryID { get; set; }
        public decimal Quantity { get; set; }
        public decimal kg { get; set; }
        public string InvoiceNo { get; set; }
        public string ProductDescription { get; set; }
        public decimal Rate { get; set; }
        public decimal SubTotal { get; set; }
        public Master.Product Product { get; set; }
        public string StatusFlag { get; set; }
    }

    public class INLREntryFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
}
