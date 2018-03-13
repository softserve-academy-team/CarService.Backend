using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CarService.DbAccess.Entities
{
    public class Mechanic : Customer, IEntity
    {
        public int WorkExperience { get; set; }
        public string Specialization { get; set; }
        public double MechanicRate { get; set; }

        public ICollection<ReviewProposition> ReviewPropositions { get; set; }
        public ICollection<Order> OrdersTaken { get; set; }
        public ICollection<Review> MadeReviews { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public new ICollection<Dialog> Dialogs { get; set; }

        public Mechanic()
        {
            ReviewPropositions = new Collection<ReviewProposition>();
            OrdersTaken = new Collection<Order>();
            MadeReviews = new Collection<Review>();
            Invitations = new Collection<Invitation>();
            Dialogs = new Collection<Dialog>();
        }
    }
}
