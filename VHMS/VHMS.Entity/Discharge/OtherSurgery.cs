using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class OtherSurgery
    {
        public int OtherSurgeryID { get; set; }
        public int DischargeEntryID { get; set; }
        public string OtherSurgeryName { get; set; }
        public DateTime OtherSurgeryDate { get; set; }
        public string sOtherSurgeryDate { get; set; }
        public string StatusFlag { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public OtherProcedure OtherProcedure { get; set; }
    }
}
