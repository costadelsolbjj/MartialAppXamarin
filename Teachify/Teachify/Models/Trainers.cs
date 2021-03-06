﻿using System;
using System.Collections.Generic;

namespace MartialApp.Models
{
    public partial class Trainers
    {
        public Trainers()
        {
            TrainerPayment = new HashSet<TrainerPayment>();
        }

        public int TrainerId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LastName2 { get; set; }
        public int? SchoolId { get; set; }
        public int? BeltId { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Created { get; set; }
        public string Tarifa { get; set; }
        public double? Importe { get; set; }

        public virtual ICollection<TrainerPayment> TrainerPayment { get; set; }
        public Guid? UserGuid { get; set; }

        public ICollection<EventTrainer> EventTrainer { get; set; }
    }
}
