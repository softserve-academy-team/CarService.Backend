using System.Threading.Tasks;
using CarService.Api.Models.DTO;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarService.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProfileService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task EditCustomerProfile(CustomerDTO customerDTO)
        {
            var user = await _userManager.FindByEmailAsync(customerDTO.Email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Customer> repository = unitOfWork.Repository<Customer>();
                    var customer = repository.Get(user.EntityId);
                    customer.FirstName = customerDTO.FirstName;
                    customer.LastName = customerDTO.LastName;
                    customer.PhoneNumber = customerDTO.PhoneNumber;
                    customer.City = customerDTO.City;
                    customer.CardNumber = customerDTO.CardNumber;

                    repository.Attach(customer);
                    unitOfWork.Save();
                }
            }
        }

        public async Task EditMechanicProfile(MechanicDTO mechanicDTO)
        {
            var user = await _userManager.FindByEmailAsync(mechanicDTO.Email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Mechanic> repository = unitOfWork.Repository<Mechanic>();
                    var mechanic = repository.Get(user.EntityId);
                    mechanic.FirstName = mechanicDTO.FirstName;
                    mechanic.LastName = mechanicDTO.LastName;
                    mechanic.PhoneNumber = mechanicDTO.PhoneNumber;
                    mechanic.City = mechanicDTO.City;
                    mechanic.CardNumber = mechanicDTO.CardNumber;

                    repository.Attach(mechanic);
                    unitOfWork.Save();
                }
            }
        }
    }
}