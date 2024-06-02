using System;
using System.Collections.Generic;

namespace SportClubs1.Data;

public partial class Staff
{
    public int StaffId { get; set; }

    public int ScheduleId { get; set; }

    public string GymAddress { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Sername { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public byte[]? Pfp { get; set; }
    public string? Info { get; set; }

    public virtual Gym GymAddressNavigation { get; set; } = null!;

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
