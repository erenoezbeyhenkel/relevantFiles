using System.ComponentModel.DataAnnotations;

namespace Hcb.Rnd.Pwn.Common.Enums;

public enum EditorMode : byte
{
    [Display(Name = "Create")]
    Create = 1,

    [Display(Name = "Update")]
    Update = 2,

    [Display(Name = "Delete")]
    Delete = 3,

    [Display(Name = "View")]
    View = 4,

    [Display(Name = "Select")]
    Select = 5,

    [Display(Name = "Send")]
    Send = 6,

    [Display(Name = "Update Version")]
    VersionUpdate = 7,

    [Display(Name = "Ok")]
    Info = 8
}
