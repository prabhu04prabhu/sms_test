using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class Drug
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string DrugCode { get; set; }
        //Commented on 01-09-2017
        //public string Dosage { get; set; }
        //public string Duration { get; set; }
        public string Instruction { get; set; }
        public string Ingredient { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //Commented on 24-10-2017
        //public Int16 FrequencyID { get; set; }
        //Added on 28-07-2017
        public Int16 InstructionID { get; set; }
        //Added on 01-09-2017
        public Dosage Dosage { get; set; }
        public Duration Duration { get; set; }
        //Added on 24-10-2017
        public Frequency Frequency { get; set; }
    }
}
