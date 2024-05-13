using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class HipReplacement
    {
        public int HipReplacementID { get; set; }
        //public int SNo { get; set; }
        public int DischargeEntryID { get; set; }
        public string HipSurgeryName { get; set; }
        public DateTime HipSurgeryDate { get; set; }
        public string sHipSurgeryDate { get; set; }
        //Modified Column Name on 20-07-2017 @ Vijaya Hospital
        public string LHipImplant { get; set; }
        public string LAcetabulumCup { get; set; }
        public string LLiner { get; set; }
        public string LFemoralStem { get; set; }
        public string LFemoralHead { get; set; }
        public string StatusFlag { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //Added new columns for Right Hip on 20-07-2017
        public string RHipImplant { get; set; }
        public string RAcetabulumCup { get; set; }
        public string RLiner { get; set; }
        public string RFemoralStem { get; set; }
        public string RFemoralHead { get; set; }
        public int HipSurgeryTypeID { get; set; }
        public string HipSurgeryTypeName { get; set; }
        //Added on 15-09-2017
        public string LAcetabulumCupTitle { get; set; }
        public string LLinerTitle { get; set; }
        public string LFemoralStemTitle { get; set; }
        public string LFemoralHeadTitle { get; set; }
        public string RAcetabulumCupTitle { get; set; }
        public string RLinerTitle { get; set; }
        public string RFemoralStemTitle { get; set; }
        public string RFemoralHeadTitle { get; set; }
    }
}
