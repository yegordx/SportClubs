using System;
using System.Collections.Generic;

namespace SportClubs1.Data;

public partial class Subscription
{
    public int Id { get; set; }

    public DateOnly? Term { get; set; }

    public int? Cost { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
