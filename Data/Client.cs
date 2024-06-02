using System;
using System.Collections.Generic;

namespace SportClubs1.Data;

public partial class Client
{
    public int Id { get; set; }

    public int SubscriptionId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public double? Weight { get; set; }

    public int? Height { get; set; }

    public int Age { get; set; }

    public byte[]? Pfp { get; set; }
    public string? Login { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual ICollection<Gym> GymAddresses { get; set; } = new List<Gym>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
