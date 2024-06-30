using System.ComponentModel;

namespace Weblog.Domain.Entities.UserAggregate;

public enum EGender
{
    [Description("Not specified")]
    None = 0,

    [Description("Male")]
    Male = 1,

    [Description("Female")]
    Female = 2
}