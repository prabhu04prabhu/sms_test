using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class patient
    {
        public int patientID { get; set; }
        public string HName { get; set; }
        public string WName { get; set; }
        public DateTime HDOB { get; set; }
        public string sHDOB { get; set; }
        public DateTime WDOB { get; set; }
        public string sWDOB { get; set; }
        public string HBloodGroup { get; set; }
        public string WBloodGroup { get; set; }
        public string HReferredBy { get; set; }
        public string WReferredBy { get; set; }
        public string HEmail { get; set; }
        public string WEmail { get; set; }
        public string HAddress { get; set; }
        public string WAddress { get; set; }
        public string HMobileNo { get; set; }        
        public string WMobileNo { get; set; }
        public string HPincode { get; set; }
        public string WPincode { get; set; }
        public string HNationality { get; set; }
        public string WNationality { get; set; }
        public string HCity { get; set; }
        public string WCity { get; set; }
        public string HCountry { get; set; }
        public string WCountry { get; set; }
        public int HCountryID { get; set; }
        public int WCountryID { get; set; }
        public int HAge { get; set; }
        public int WAge { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string OPDNo { get; set; }
        public string HImage { get; set; }
        public string HProfession { get; set; }
        public string WProfession { get; set; }
        public string WImage { get; set; }
        public string Category { get; set; }
        public string HIDProof { get; set; }
        public string WIDProof { get; set; }
        public string HReferredDetails { get; set; }
        public string WReferredDetails { get; set; }
        public string LastOPDNo { get; set; }
        public string RefDoctorMobileNo { get; set; }
       
        
    }

    public class PatientFilter
    {
        public int PatientID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Category { get; set; }
        public string ReferredBy { get; set; }
    }
}
