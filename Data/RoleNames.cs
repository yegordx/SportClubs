using System.Collections.Generic;
namespace SportClubs1.Data;
public static class RoleNames
{
    public static ICollection<string> All => new List<string>
    {
        "Client",
        "Staff",
        "Administrator"
    };
}
