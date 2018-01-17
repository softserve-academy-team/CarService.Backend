using CarService.DbAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DataAccess.Model
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public UserStatus Status { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
