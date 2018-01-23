using System.Collections.Generic;

namespace CarService.DbAccess.Entities
{
    public class Customer : User
    {
        public string City { get; set; }
        public string CardNumber { get; set; }

        public virtual ICollection<Order> OrdersMade { get; set; }
        public virtual ICollection<CustomerAuto> CustomerAutoes { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }
    }
}
