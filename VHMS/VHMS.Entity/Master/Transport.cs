using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Transport
    {
        public int TransportID { get; set; }
        public string TransportName { get; set; }
        public string GSTNo { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string VehicleNo { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
