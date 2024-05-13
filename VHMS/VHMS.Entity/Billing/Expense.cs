using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string ExpenseNo { get; set; }
       
      public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string sExpenseDate { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate { get; set; }
        public Ledger Party { get; set; }
        public FinancialYear FinancialYear { get; set; }
        public Tax Tax { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AdditionalDiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Roundoff { get; set; }
        public string GSTIN { get; set; }
        public string Narration { get; set; }
        public string BillNo { get; set; }
        public Int16 ReceiptModeID { get; set; }
        public Ledger Bank { get; set; }
        public string ChequeNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string sIssueDate { get; set; }
        public string Status { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CUserName { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal OnAmount { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public Collection<ExpenseTrans> ExpenseTrans { get; set; }
    }

    public class ExpenseTrans
    {
        public int ExpenseTransID { get; set; }
        public int ExpenseID { get; set; }
        public decimal Amount { get; set; }
        public Ledger Ledger { get; set; }
        public Tax Tax { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CGSTPercent { get; set; }
        public decimal SGSTPercent { get; set; }
        public decimal IGSTPercent { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public string StatusFlag { get; set; }
    }

    public class ExpenseFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int DoctorID { get; set; }
        public int UserID { get; set; }
    }
}
