using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int DischargeEntryID { get; set; }
        public Drug Drug { get; set; }
        public Int16 InstructionType { get; set; }
        public string Ingredient { get; set; }
        public string StatusFlag { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //Added on 01-09-2017
        public int DosageID { get; set; }
        public string Dosage { get; set; }
        //Modified Data Type int16 to int32 on 24-10-2017
        public int FrequencyID { get; set; }
        public string Frequency { get; set; }
        public int DurationID { get; set; }
        public string Duration { get; set; }
        //Added on 04-09-2017
        public string OtherFrequency { get; set; }
        public string Instruction { get; set; }
    }
}
