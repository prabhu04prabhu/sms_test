using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class SMSLog
    {
        public int SMSLogID { get; set; }
        public DateTime SMSLogDate { get; set; }
        public string sSMSLogDate { get; set; }
        public string Message { get; set; }
        public string SendTo { get; set; }
        public User CreatedBy { get; set; }

    }
   
}
