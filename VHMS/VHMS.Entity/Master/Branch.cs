using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public Region Region { get; set; }
        public Zone Zone { get; set; }
        public string ContactPerson { get; set; }
        public Boolean HeadOfficeFlag { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
