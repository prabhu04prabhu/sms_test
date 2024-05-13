using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class FinancialYear
    {
        public int FinancialYearID { get; set; }
        public string Title { get; set; }
        public DateTime YearFrom { get; set; }
        public DateTime YearTo { get; set; }     
        public string sYearFrom { get; set; }
        public string Status { get; set; }
        public Boolean Active { get; set; }
        public string sYearTo { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
