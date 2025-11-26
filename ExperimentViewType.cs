using System.ComponentModel.DataAnnotations;

namespace Hcb.Rnd.Pwn.Common.Enums;

public enum ExperimentViewType : byte
{
    [Display(Name = "Product Developer View")]
    ProductDeveloper = 1,

    [Display(Name = "Validator View")]
    Validator = 2,

    [Display(Name = "Operator View")]
    Operator = 4
}
