using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class PunchLog
    {

        public int PunchLogID { get; set; }
        public Employee Employee { get; set; }
        public DateTime LogDate { get; set; }
        public string sLogDate { get; set; }
        public string LogTime { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}