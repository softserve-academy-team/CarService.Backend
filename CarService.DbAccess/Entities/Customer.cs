using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CarService.DbAccess.Entities
{
    public class Customer : User
    {
        public string City { get; set; }
        public string CardNumber { get; set; }

        public ICollection<Order> OrdersMade { get; set; }
        public ICollection<CustomerAuto> CustomerAutoes { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Dialog> Dialogs { get; set; }

        public Customer()
        {
            OrdersMade = new Collection<Order>();
            CustomerAutoes = new Collection<CustomerAuto>();
            Reviews = new Collection<Review>();
            Dialogs = new Collection<Dialog>();
        }
    }
}
