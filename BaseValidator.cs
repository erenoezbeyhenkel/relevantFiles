using FluentValidation;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;

namespace Hcb.Rnd.Pwn.Application.Common.Validator;

public abstract class BaseValidator<TValidationObject> : AbstractValidator<TValidationObject>
{
    public IPwnHttpContextAccessor PwnHttpContextAccessor { get; }

    public BaseValidator(IPwnHttpContextAccessor pwnHttpContextAccessor)
    {
        PwnHttpContextAccessor = pwnHttpContextAccessor;
    }
    public BaseValidator()
    {

    }

    /// <summary>
    /// Check the request group id and user groups. User groups have to include requested group id.
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    //public bool BeAuthorized(string groupId)
    //{
    //    if (Guard.Against.IsNull(PwnHttpContextAccessor.User))
    //        return false;

    //    if (Guard.Against.IsNull(PwnHttpContextAccessor.Identity))
    //        return false;

    //    if (PwnHttpContextAccessor.IsAuthenticated.HasValue && !PwnHttpContextAccessor.IsAuthenticated.Value)
    //        return false;

    //    if (PwnHttpContextAccessor.UserGroups.Count == 0)
    //        return false;

    //    return PwnHttpContextAccessor.UserGroups.Contains(groupId);
    //}
}
