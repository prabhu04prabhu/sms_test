using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class AttendanceLog
    {

        public int AttendanceLogID { get; set; }
        public Employee Employee { get; set; }
        public DateTime PunchDate { get; set; }
        public string sPunchDate { get; set; }
        public string PunchInTime { get; set; }
        public string PunchOutTime { get; set; }
        public string Duration { get; set; }
        public decimal DeductionAmt { get; set; }
        public int LateMinutes { get; set; }
        public string SpecialStatus { get; set; }
        public string Status { get; set; }
        public Boolean Active { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OvertimeCount { get; set; }
        public int Edit { get; set; }
    }
}