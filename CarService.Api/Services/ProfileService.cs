using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _iMapper;

        public ProfileService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory, IMapper iMapper)
        {
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _iMapper = iMapper;
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

                    _iMapper.Map<CustomerDTO, Customer>(customerDTO, customer);

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

                    _iMapper.Map<MechanicDTO, Mechanic>(mechanicDTO, mechanic);

                    repository.Attach(mechanic);
                    unitOfWork.Save();
                }
            }
        }

        public async Task<UserDTO> GetUserDTO(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                if (user is Mechanic)
                {
                    using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                    {
                        IRepository<Mechanic> repository = unitOfWork.Repository<Mechanic>();
                        var mechanic = repository.Get(user.EntityId);
                        return _iMapper.Map<Mechanic, MechanicDTO>(mechanic);
                    }
                }

                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Customer> repository = unitOfWork.Repository<Customer>();
                    var customer = repository.Get(user.EntityId);
                    return _iMapper.Map<Customer, CustomerDTO>(customer);
                }
            }

            return null;
        }
    }
}