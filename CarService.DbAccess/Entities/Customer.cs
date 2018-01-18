using CarService.DataAccess.Model;
using CarService.DbAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarService.DataAccess.Model
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
