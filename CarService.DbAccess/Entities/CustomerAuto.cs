using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.DataAccess.Model
{
    public class CustomerAuto
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int AutoId { get; set; }
        public Auto Auto { get; set; }
    }
}
