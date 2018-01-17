using CarService.DataAccess.Model;
using CarService.DbAccess.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Model
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