using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Master
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public Billing.Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public Section Section { get; set; }
        public int MinimumStock { get; set; }
        public Billing.Unit Unit { get; set; }
        public Billing.Supplier Supplier { get; set; }
        public string SMSCode { get; set; }
        public string HSNCode { get; set; }
        public string DesignNo { get; set; }
        public string ProductImage1 { get; set; }
        public string PrintName { get; set; }
        public string SupplierProductName { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string sCreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int TaxID { get; set; }
        public Billing.Tax Tax { get; set; }
        public Billing.Tax TaxPercent { get; set; }
        public int ProductTypeID { get; set; }
        public int IsRateUpdated { get; set; }
        public Billing.ProductType ProductType { get; set; }
        public int MaximumStock { get; set; }
        public Boolean PricingA { get; set; }
        public Boolean PricingB { get; set; }
        public Boolean PricingC { get; set; }
        public string GenerateSMSCode { get; set; }
        public Collection<ProductImages> ProductImages { get; set; }
    }
    public class ProductImages
    {
        public int ImageID { get; set; }
        public int ProductID { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }

    }
}
