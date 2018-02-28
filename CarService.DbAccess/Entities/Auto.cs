using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Auto : IEntity
    {
        public int Id { get; set; }
        public int AutoRiaId { get; set; }
        public string Info { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<CustomerAuto> CustomerAutoes { get; set; }

        public Auto()
        {
            Orders = new Collection<Order>();
            CustomerAutoes = new Collection<CustomerAuto>();
        }
    }
}