using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Gift
    {
        public int GiftID { get; set; }
        public string GiftName { get; set; }
        public string GiftCode { get; set; }
        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
