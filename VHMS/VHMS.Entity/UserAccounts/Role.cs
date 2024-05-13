using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean CommonMargin { get; set; }
        public Boolean MarginView { get; set; }
        public int RoleConfig_Count { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class RoleConfiguration
    {
        public int RoleConfigurationID { get; set; }
        public Role Role { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public Boolean IsAccess { get; set; }
        public Boolean IsView { get; set; }
        public Boolean IsAdd { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsDelete { get; set; }
        public string StatusFlag { get; set; }
    }
}
