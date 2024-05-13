using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class PrescriptionMaster
    {
        public int PrescriptionID { get; set; }
        public string PrescriptionNo { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string sPrescriptionDate { get; set; }
        public patient Patient { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Collection<PrescriptionTrans> PrescriptionTrans { get; set; }
    }

    public class PrescriptionTrans
    {
        public int PrescriptionTransID { get; set; }
        public int PrescriptionID { get; set; }
        public int DrugID { get; set; }
        //public int InstructionType { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Instruction { get; set; }
        public string Ingredient { get; set; }
        public string StatusFlag { get; set; }
        public Discharge.Drug Drug { get; set; }
        public Int16 InstructionType { get; set; }
        public int DosageID { get; set; }
        public int FrequencyID { get; set; }
        public int DurationID { get; set; }
    }
}
