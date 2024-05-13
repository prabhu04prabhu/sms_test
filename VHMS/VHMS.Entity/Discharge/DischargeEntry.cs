using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Discharge
{
    public class DischargeEntry
    {
        public int DischargeEntryID { get; set; }
        public DateTime DischargeDateTime { get; set; }        
        public string sDischargeDate { get; set; }
        public string sDischargeTime { get; set; }
        public string CoConsultantID { get; set; }
        public string Registrar { get; set; }
        public string ExternalDoctor { get; set; }
        public string DrugAllergy { get; set; }
        public string Diagnosis { get; set; }
        public string CourseDuringStay { get; set; }
        public string Investigation { get; set; }
        public string PastHistory { get; set; }
        public string GeneralExamination { get; set; }
        public string LocalExamination { get; set; }
        public string AdviseonDischarge { get; set; }
        public DateTime ReviewAppointmentDateTime { get; set; }
        public string sReviewAppointmentDate { get; set; }
        public string sReviewAppointmentTime { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Collection<HipReplacement>  HipReplacement { get; set; }
        public Collection<KneeReplacement> KneeReplacement { get; set; }
        public Collection<OtherSurgery> OtherSurgery { get; set; }
        public Collection<Prescription> Prescription { get; set; }
        //Added on 13-07-2017
        public Admission Admission { get; set; }
        //Added on 29-07-2017
        public DateTime SurgeryDate { get; set; }
        public string sSurgeryDate { get; set; }
        public string RegistrarID { get; set; }
        //Added on 01-08-2017
        public Int16 SummaryTypeID { get; set; }
        //Added on 04-09-2017
        public string CauseofDeath { get; set; }

        //Added on 05-09-2017
        public string WrittenBy { get; set; }
        public int CheckedBy { get; set; }
        public string CheckedByName { get; set; }
        public string WeekDays { get; set; }
        public string sWeekDays { get; set; }
        //Added on 08-09-2017
        public Collection<PatientOwnDrug> PatientOwnDrug { get; set; }
    }
}
