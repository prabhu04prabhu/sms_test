using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity.Discharge
{
    public class Specialization
    {
        public int SpecializationID { get; set; }
        public string SpecializationName { get; set; }
        public string SpecializationCode { get; set; }        
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
