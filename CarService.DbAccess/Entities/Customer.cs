using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CarService.DbAccess.Entities
{
    public class Customer : User
    {
        public string City { get; set; }
        public string CardNumber { get; set; }

        public ICollection<Order> OrdersMade { get; set; }
        public ICollection<Dialog> Dialogs { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

        public Customer()
        {
            OrdersMade = new Collection<Order>();
            Favorites = new Collection<Favorite>();
            Dialogs = new Collection<Dialog>();
        }
    }
}
