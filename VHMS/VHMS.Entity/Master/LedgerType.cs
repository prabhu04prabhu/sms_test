using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class LedgerType
    {
        public int LedgerTypeID { get; set; }
        public int PrimaryGroupID { get; set; }
        public string LedgerTypeName { get; set; }
        public string RecordType { get; set; }
        public string PrimaryGroupName { get; set; }
        public Boolean IsDefaultRecord { get; set; }
        public Boolean IsSubGroup { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
