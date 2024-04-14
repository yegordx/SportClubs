using System;
using System.Collections.Generic;

namespace SportClubs1.Data;

public partial class TrainingMachine
{
    public int InventNum { get; set; }

    public string GymAddress { get; set; } = null!;

    public int Cost { get; set; }

    public string? State { get; set; }

    public string? Name { get; set; }

    public virtual Gym GymAddressNavigation { get; set; } = null!;
}
