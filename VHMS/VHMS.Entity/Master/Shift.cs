﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Shift
    {
      
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
