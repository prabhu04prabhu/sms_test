﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class District
    {
        public State State { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string DistrictCode { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}