using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Settings
    {
        public decimal MaxDiscountPercent { get; set; }
        public DateTime OpeningDate { get; set; }
        public string sOpeningDate { get; set; }
        public Boolean SendSMS { get; set; }
        public string SenderName { get; set; }
        public string APILink { get; set; }
        public string SMSUsername { get; set; }
        public string SMSPassword { get; set; }
        public int Messagesentcnt { get; set; }
        public int NotificationSentCount { get; set; }
  	    public decimal SalesTaxAmount { get; set; }
        public decimal MaxSalesDiscount { get; set; }
        public decimal MaxSalesDiscountPercent { get; set; }
        public decimal WholeSaleMinMargin { get; set; }
        public decimal RetailMinMargin { get; set; }
        public string HostName { get; set; }
        public string Port { get; set; }
        public string CompanyName { get; set; }
        public string UserMailPassword { get; set; }
        public string UserMailID { get; set; }
        public Boolean DefaultCrendentials { get; set; }
        public Boolean EnableSSL { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string CSTNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string ContactEmail { get; set; }
        public Ledger PaymentBank { get; set; }
        public int PaymentModeID { get; set; }
        public Ledger ReceiptBank { get; set; }
        public int ReceiptModeID { get; set; }
        public Ledger ExpenseBank { get; set; }
        public int ExpensePaymentModeID { get; set; }
        public Ledger OtherExpenseBank { get; set; }
        public int OtherReceiptModeID { get; set; }
        public Ledger RetailReceiptBank { get; set; }
        public int RetailReceiptModeID { get; set; }
        public string Retails_Sales_MobileNo { get; set; }
        public string Retails_Sales_Email { get; set; }
        public string Retails_Sales_GSTNO { get; set; }
        public string Retails_Sales_Address { get; set; }

    }
   
}
