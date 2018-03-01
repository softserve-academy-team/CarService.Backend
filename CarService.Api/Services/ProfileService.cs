using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Api.Models;
using CarService.Api.Models.DTO;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace CarService.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _iMapper;

        private readonly ICarService _autoRiaCarService;

        public ProfileService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory, IMapper iMapper, ICarService autoRiaCarService)
        {
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _iMapper = iMapper;
            _autoRiaCarService = autoRiaCarService;
        }

        public async Task EditCustomerProfile(string email, CustomerDto customerDto)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Customer> repository = unitOfWork.Repository<Customer>();
                    var customer = repository.Get(user.Id);

                    _iMapper.Map<CustomerDto, Customer>(customerDto, customer);

                    await unitOfWork.SaveAsync();
                }
            }
        }

        public async Task EditMechanicProfile(string email, MechanicDto mechanicDto)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Mechanic> repository = unitOfWork.Repository<Mechanic>();
                    var mechanic = repository.Get(user.Id);

                    _iMapper.Map<MechanicDto, Mechanic>(mechanicDto, mechanic);

                    await unitOfWork.SaveAsync();
                }
            }
        }

        public async Task<Models.DTO.UserDto> GetUserDTO(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    if (user is Mechanic)
                    {
                        IRepository<Mechanic> repository = unitOfWork.Repository<Mechanic>();
                        var mechanic = repository.Get(user.Id);
                        return _iMapper.Map<Mechanic, MechanicDto>(mechanic);
                    }
                    else
                    {
                        IRepository<Customer> repository = unitOfWork.Repository<Customer>();
                        var customer = repository.Get(user.Id);
                        return _iMapper.Map<Customer, CustomerDto>(customer);
                    }
                }
            }

            return null;
        }

        public async Task AddCarToFavorites(string email, int autoRiaId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Favorite> customerAutos = unitOfWork.Repository<Favorite>();
                    customerAutos.Add(new Favorite { CustomerId = user.Id, AutoRiaId = autoRiaId });

                    await unitOfWork.SaveAsync();
                }
            }
        }

        public async Task DeleteCarFromFavorites(string email, int autoRiaId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Favorite> customerAutos = unitOfWork.Repository<Favorite>();
                    customerAutos.Delete(new Favorite { CustomerId = user.Id, AutoRiaId = autoRiaId });

                    await unitOfWork.SaveAsync();
                }
            }
        }

        public async Task<IEnumerable<BaseCarInfo>> GetAllCarsFromFavorites(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Favorite> favorites = unitOfWork.Repository<Favorite>();

                    var carIds = from f in favorites.Query()
                                 where f.CustomerId == user.Id
                                 select f.AutoRiaId;

                    return await _autoRiaCarService.GetBaseInfoAboutCars(carIds);
                }
            }
            else return null;
        }

        public async Task<bool> IsCarInFavorites(string email, int autoRiaId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Favorite> favorites = unitOfWork.Repository<Favorite>();

                    if (favorites.Find(f => f.CustomerId == user.Id && f.AutoRiaId == autoRiaId) != null)
                        return true;
                    else return false;
                }
            }
            else return false;
        }

        public async Task<IEnumerable<ProfileOrderInfo>> GetUserCreatedOrders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var orderRepository = unitOfWork.Repository<Order>();
                var autoRepository = unitOfWork.Repository<Auto>();

                var orders = from o in orderRepository.Query()
                             where o.CustomerId == user.Id
                             join a in autoRepository.Query() on o.AutoId equals a.Id
                             orderby o.Status, o.Date descending
                             select new ProfileOrderInfo
                             {
                                 OrderId = o.Id,
                                 Date = o.Date.ToString("dd-MM-yyyy"),
                                 Status = o.Status.ToString(),
                                 MarkName = a.MarkName,
                                 ModelName = a.ModelName,
                                 Year = a.Year,
                                 PhotoLink = a.PhotoLink
                             };

                return orders.ToList();
            }
        }

        public async Task<IEnumerable<ProfileOrderInfo>> GetUserAppliedOrders(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var orderRepository = unitOfWork.Repository<Order>();
                var autoRepository = unitOfWork.Repository<Auto>();
                var propositionRepository = unitOfWork.Repository<ReviewProposition>();

                var orders = from r in propositionRepository.Query()
                             where r.MechanicId == user.Id
                             join o in orderRepository.Query() on r.OrderId equals o.Id
                             join a in autoRepository.Query() on o.AutoId equals a.Id
                             orderby o.Status, o.Date descending
                             select new ProfileOrderInfo
                             {
                                 OrderId = o.Id,
                                 Date = o.Date.ToString("dd-MM-yyyy"),
                                 Status = o.Status.ToString(),
                                 MarkName = a.MarkName,
                                 ModelName = a.ModelName,
                                 Year = a.Year,
                                 PhotoLink = a.PhotoLink,
                                 IsDoIt = (o.Status ==  0 || (o.Status > 0 && o.MechanicId == user.Id)) ? true : false
                             };

                return orders.ToList();
            }
        }
    }
}