﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Dialog : IEntity
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Dialog()
        {
            Messages = new Collection<Message>();
        }
    }
}
