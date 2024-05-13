using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class Admission
    {
        public int AdmissionID { get; set; }
        public string AdmissionNo { get; set; }
        public string UHIDNo { get; set; }
        public string RoomNo { get; set; }
        public string ContactNo { get;set; }
        public string PatientName { get; set; }
        public Int16 PatientAge { get; set; }
        public Int16 PatientSex { get; set; }
        public string PrimaryConsultant { get; set; }
        public DateTime DateofAdmission { get; set; }
        public string sDateofAdmissionDate { get; set; }
        public string sDateofAdmissionTime { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //Added on 28-07-2017
        public string MLCNo { get; set; }
        public string PatientAddress { get; set; }
        //Added on 29-07-2017
        public string PrimaryConsultantID { get; set; }
        //Added on 17-09-2017
        public string SummaryType { get; set; }
    }
}

