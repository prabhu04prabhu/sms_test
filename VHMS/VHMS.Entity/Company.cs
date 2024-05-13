using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyAddress { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string LandLine { get; set; }
        public string Fax { get; set; }
        public string TINNo { get; set; }
        public string CINNo { get; set; }
        public string CSTNo { get; set; }
        public string IEC { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal MaxDiscountPercent { get; set; }
        public decimal SalesTaxAmount { get; set; }
        public Boolean SendSMS { get; set; }
        public string SenderName { get; set; }
        public string APILink { get; set; }
        public string SMSUsername { get; set; }
        public string SMSPassword { get; set; }
        public string FinancialYear { get; set; }
        public string Tream { get; set; }
        public string CompanyNote { get; set; }
        public decimal Cash { get; set; }
        public decimal Credit { get; set; }
        public decimal DebitCard { get; set; }
        public decimal CreditCard { get; set; }
        public decimal GPay { get; set; }
        public decimal Paytm { get; set; }
        public decimal IMPS { get; set; }
        public decimal NEFT { get; set; }
        public decimal Cheque { get; set; }
        public decimal Advance { get; set; }
        public decimal Others { get; set; }
        public decimal TodayPayment { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
    }
}
