using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Models.SManagement.Permission
{
    public class PermissionResponse
    {
        public int PermissionID { get; set; }
        public string PageName { get; set; }
        public string PagePath { get; set; }
        public int ModuleID { get; set; }
        public bool ViewYn { get; set; }
        public bool AddYn { get; set; }
        public bool EditYn { get; set; }
        public bool DeleteYn { get; set; }
        public bool ReviewYn { get; set; }
        public bool ApproveYn { get; set; }
        public bool PayYn { get; set; }
        public bool PostYn { get; set; }
        public bool CopyYn { get; set; }
        public bool ProcessYn { get; set; }
        public bool SubmitYn { get; set; }
        public bool SaveYn { get; set; }
        public bool PrintYn { get; set; }
        public bool VoidYN { get; set; }
        public bool ChangePdFyYn { get; set; }
        public bool BypassCashAccountNotAfilliated { get; set; }
        public bool BypassGLRestrictedYn { get; set; }
        public bool BypassGLEditedYn { get; set; }
        public bool UnpostYN { get; set; }
        public bool EditImageYN { get; set; }
        public string UserTemplate { get; set; }
        public bool MoreActionsYn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int ImageID { get; set; }
        public int Sort { get; set; }
        public string CreatedUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
