using System.Collections.Generic;

namespace CarService.DbAccess.Entities
{
    public class Mechanic : Customer
    {
        public int MechanicRate { get; set; }

        public virtual ICollection<ReviewProposition> ReviewPropositions { get; set; }

        public virtual ICollection<Order> OrdersTaken { get; set; }

        public virtual ICollection<Review> MadeReviews { get; set; }

        public new ICollection<Dialog> Dialogs{ get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
