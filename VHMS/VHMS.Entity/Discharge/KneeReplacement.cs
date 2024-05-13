using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class KneeReplacement
    {
        public int KneeReplacementID { get; set; }
        public int DischargeEntryID { get; set; }
        public int KneeSurgeryTypeID { get; set; }
        public string KneeSurgeryName { get; set; }
        public DateTime KneeSurgeryDate { get; set; }        
        public string sKneeSurgeryDate { get; set; }
        //Modified on 20-07-2017
        public string LKneeImplant { get; set; }
        public string LFemur { get; set; }
        public string LTibia { get; set; }
        public string LPoly { get; set; }
        public string LStem { get; set; }
        public string StatusFlag { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //Added on 20-07-2017
        public string RKneeImplant { get; set; }
        public string RFemur { get; set; }
        public string RTibia { get; set; }
        public string RPoly { get; set; }
        public string RStem { get; set; }
        //Added on 15-09-2017
        //LEFT Title
        public string LFemurTitle { get; set; }
        public string LTibiaTitle { get; set; }
        public string LPolyTitle { get; set; }
        public string LStemTitle { get; set; }
        //RIGHT Title
        public string RFemurTitle { get; set; }
        public string RTibiaTitle { get; set; }
        public string RPolyTitle { get; set; }
        public string RStemTitle { get; set; }
    }
}
