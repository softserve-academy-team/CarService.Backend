using System;
using System.Collections.Generic;
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
                    var customer = repository.Get(user.Id);

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
                    var mechanic = repository.Get(user.Id);

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
                        var mechanic = repository.Get(user.Id);
                        return _iMapper.Map<Mechanic, MechanicDTO>(mechanic);
                    }
                }

                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Customer> repository = unitOfWork.Repository<Customer>();
                    var customer = repository.Get(user.Id);
                    return _iMapper.Map<Customer, CustomerDTO>(customer);
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
                    customerAutos.Add(new Favorite {CustomerId = user.Id, AutoRiaId = autoRiaId });

                    unitOfWork.Save();
                }
            }
        }

        public async Task<IEnumerable<ProfileOrderInfo>> GetUserCreatedOrders(string email)
        {
            return new List<ProfileOrderInfo>
            {
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Canceled.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Active.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Pending.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2012", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2010", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"}
            };
        }

        public async Task<IEnumerable<ProfileOrderInfo>> GetUserAppliedOrders(string email)
        {
            return new List<ProfileOrderInfo>
            {
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Canceled.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Active.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Pending.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2008", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2012", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"},
                new ProfileOrderInfo { OrderId = 12345, Date = DateTime.Now.ToString("dd-MM-yyyy"), Status = OrderStatus.Done.ToString(), MarkName = "Mercedes", ModelName = "G250", Year = "2010", PhotoLink = "https://img-c.drive.ru/models.photos/0000/000/000/000/de2/48d45147112b1286-large.jpg"}
            };
        }
    }
}