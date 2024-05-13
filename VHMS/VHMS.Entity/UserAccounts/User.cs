using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class User
    {
        public int UserID { get; set; }
        public int UserID1 { get; set; }
        public int BranchID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public Region Region { get; set; }
        public Zone Zone { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int16 HomePageID { get; set; }
        public string PageName { get; set; }
        public string FileName { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public decimal BasicPay { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DOJ { get; set; }
        public string sDOJ { get; set; }
        public string sDOB { get; set; }
        public DateTime DOB { get; set; }
        //public string SalesManCode { get; set; }
        public string EmployeeCode { get; set; }
        public Branch Branch { get; set; }
        public Discharge.Designation Designation { get; set; }
        public string IDProof { get; set; }
        public string MobileNo { get; set; }
        public string Title { get; set; }
        public int FinancialYearID { get; set; }
        public Settings Settings { get; set; }
    }
    public class VisitLog
    {
        public int PageLogID { get; set; }
        public DateTime VisitLogDateTime { get; set; }
        public string sVisitLogDateTime { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string URL { get; set; }
    }
}
