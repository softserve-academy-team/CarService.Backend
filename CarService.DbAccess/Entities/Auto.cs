using System.Collections.Generic;

namespace CarService.DbAccess.Entities
{
    public class Auto : IEntity
    {
        public int Id { get; set; }
        public string AutoRiaId { get; set; }
        public string Info { get; set; }

        public ICollection<Order> Orders { get; set; }

        public virtual ICollection<CustomerAuto> CustomerAutoes { get; set; }
    }
}