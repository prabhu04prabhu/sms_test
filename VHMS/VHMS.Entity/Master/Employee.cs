using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Employee
    {

        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime Dob { get; set; }
        public string sDob { get; set; }
        public string Address { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public DateTime Dateofjoin { get; set; }
        public string sDateofjoin { get; set; }
        public string IDProof { get; set; }
        public string BloodGroup { get; set; }
        public string ProofImage1 { get; set; }
        public string ProofImage2 { get; set; }
        public string Gender { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal Conveyance { get; set; }
        public decimal BasicPay { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal PF { get; set; }
        public decimal HRA { get; set; }
        public decimal ESI { get; set; }
        public decimal OvertimeAllowance { get; set; }
        public decimal FoodAllowance { get; set; }
        public decimal NetSalary { get; set; }
        public int PaidLeaves { get; set; }
        public int ESLID { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public string EmployeeInTime { get; set; }
        public string EmployeeOutTime { get; set; }
        public Shift Shift { get; set; }
    }
}