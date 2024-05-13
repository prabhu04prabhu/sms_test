using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class DoctorType
    {
        public int DoctorTypeID { get; set; }
        public string DoctorTypeName { get; set; }
        public string DoctorTypeCode { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
