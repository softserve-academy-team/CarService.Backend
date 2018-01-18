using CarService.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DataAccess.Model
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
