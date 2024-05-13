using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Salary
    {

        public int SalaryID { get; set; }
        public string MonthYear { get; set; }
        public string Comments { get; set; }
        public DateTime SalaryDate { get; set; }
        public string sSalaryDate { get; set; }
        public Employee Employee { get; set; }
        public decimal BasicPay { get; set; }
        public decimal Deduction { get; set; }
        public decimal Incentives { get; set; }
        public decimal NetSalary { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public Boolean Active { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public decimal AttendanceDeduction { get; set; }
        public decimal InAdvanceDeduction { get; set; }
        public decimal OvertimeIncentive { get; set; }

    }
}