using System.ComponentModel;

namespace AlChavoMobilePayroll.ViewModels.Common
{
    public partial class BaseViewModel
    {
        public enum Actions
        {
            [Description("View")]
            View,
            [Description("Add")]
            Add,
            [Description("Edit")]
            Edit,
            [Description("Delete")]
            Delete,
            [Description("Review")]
            Review,
            [Description("Approved")]
            Approved,
            [Description("Pay")]
            Pay,
            [Description("Post")]
            Post,
            [Description("Unpost")]
            Unpost,
            [Description("Void")]
            Void,
            [Description("Save")]
            Save,
            [Description("Submit")]
            Submit,
            [Description("Process")]
            Process,
            [Description("Print")]
            Print,
            [Description("Copy")]
            Copy,
            [Description("EditImage")]
            EditImage,
            [Description("More actions")]
            MoreActions,
            [Description("ChangePdFy")]
            ChangePdFy,
            [Description("BypassCashAccountNotAfilliated")]
            BypassCashAccountNotAfilliated,
            [Description("BypassGLRestricted")]
            BypassGLRestricted,
            [Description("BypassGLEdited")]
            BypassGLEdited
        }

     
    }
}
