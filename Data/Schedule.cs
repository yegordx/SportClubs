using System;
using System.Collections.Generic;

namespace SportClubs1.Data;

public partial class Schedule
{
    public int Id { get; set; }

    public int HourPerDay { get; set; }

    public bool Monday { get; set; }

    public bool Tuesday { get; set; }

    public bool Wednesday { get; set; }

    public bool Thursday { get; set; }

    public bool Friday { get; set; }

    public bool Saturday { get; set; }

    public bool Sunday { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
