using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportClubs1.Data;

public partial class Gym
{
    public string Address { get; set; } = null!;
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public string Name { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string? Info { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<TrainingMachine> TrainingMachines { get; set; } = new List<TrainingMachine>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
