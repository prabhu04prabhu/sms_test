using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string WhatsAppNo { get; set; }
        public string AlternateNo { get; set; }
        public string Email { get; set; }
        public string GSTNo { get; set; }
        public string Notes { get; set; }
        public string DOB { get; set; }
        public string CustomerType { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int StateID { get; set; }
        public string City { get; set; }
        public string Comments { get; set; }
        public string Area { get; set; }
        public string Pincode { get; set; }
        public string Shipping_Address { get; set; }
        public decimal Default_DiscountPercent { get; set; }
        public decimal Balance_amount { get; set; }
        public decimal Limit_SalesAmount { get; set; }
        public State State { get; set; }
        public int CustomertypeID { get; set; }
        public CustomerTypes CustomerTypes { get; set; }
        public Transport Transport { get; set; }
        public int MaxDueDays { get; set; }
        public int MinDueDays { get; set; }
        public int Days { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string MDType { get; set; }
        public string ManagerType { get; set; }
        public string MDName { get; set; }
        public string MDContact { get; set; }
        public string MangerName { get; set; }
        public string MangerContact { get; set; }
        public Collection<ShippingAddress> ShippingAddress { get; set; }
    }

    public class ShippingAddress
    {
        public int ShippingAddressID { get; set; }
        public int CustomerID { get; set; }
        public string Address { get; set; }
        public string BranchName { get; set; }
        public string GSTIN { get; set; }
        public string Place { get; set; }
        public string MobileNo { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public State State { get; set; }
        public string StatusFlag { get; set; }

    }
}
