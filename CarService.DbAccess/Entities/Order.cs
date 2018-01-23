using System;
using System.Collections.Generic;

namespace CarService.DbAccess.Entities
{
        public class Order : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

            public OrderStatus Status { get; set; }

            public int? CustomerId { get; set; }
            public Customer Customer { get; set; }

            public int? MechanicId { get; set; }
            public Mechanic Mechanic { get; set; }

            public int? AutoId { get; set; }
            public Auto Auto { get; set; }

            public Review Review { get; set; }

            public ICollection<Comment> Comments { get; set; }
            public ICollection<Invitation> Invitations { get; set; }
            public ICollection<ReviewProposition> ReviewPropositions { get; set; }
            public ICollection<Dialog> Dialogs { get; set; }

        }
}
