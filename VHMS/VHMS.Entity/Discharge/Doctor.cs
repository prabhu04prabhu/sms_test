using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public DoctorType DoctorType { get; set; }
        public Specialization Specialization { get; set; }
        public bool IsRMODoctor { get; set; }
        public bool IsExternalDoctor { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public State State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PhoneNo3 { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }        
        public Department Department { get; set; }
        public string DoctorNo { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        //Added on 31-07-2017
        public Int16 DisplayOrder { get; set; }
    }
}
