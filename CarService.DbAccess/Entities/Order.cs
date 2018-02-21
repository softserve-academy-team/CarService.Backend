﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Order : IEntity
    {
        [Key]
        public int EntityId { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? AutoId { get; set; }
        public Auto Auto { get; set; }

        public int? ReviewId { get; set; }
        public Review Review { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<ReviewProposition> ReviewPropositions { get; set; }
        public ICollection<Dialog> Dialogs { get; set; }

        public Order()
        {
            Comments = new Collection<Comment>();
            Invitations = new Collection<Invitation>();
            ReviewPropositions = new Collection<ReviewProposition>();
            Dialogs = new Collection<Dialog>();
        }

    }
}
