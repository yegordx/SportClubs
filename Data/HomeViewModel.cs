using SportClubs1.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportClubs1.Data;

public class HomeViewModel
{
    public List <Gym> Gyms { get; set; }
    public List<Staff> Staffs { get; set; }
    public List<TrainingMachine> TrainingMachines { get; set; }

    public List<Client> Clients { get; set; }

}

