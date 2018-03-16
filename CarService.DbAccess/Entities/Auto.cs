using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Auto : IEntity
    {
        public int Id { get; set; }
        public int AutoRiaId { get; set; }
        public int TypeId { get; set; }
        public int MarkId { get; set; }
        public int ModelId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string PhotoLink { get; set; }
        public string City { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Auto()
        {
            Orders = new Collection<Order>();
        }
    }
}