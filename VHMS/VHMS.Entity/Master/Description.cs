﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Description
    {
        
        public int DescriptionID { get; set; }
        public string DescriptionName { get; set; }
        public Decimal Amount { get; set; }
        public Boolean IsActive { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DescriptionCategory DescriptionCategory { get; set; }
    }
}
