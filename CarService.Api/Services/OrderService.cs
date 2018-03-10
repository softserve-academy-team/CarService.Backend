using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CarService.DbAccess.Entities;
using CarService.DbAccess.DAL;
using CarService.Api.Models;
using CarService.Api.Models.DTO;

namespace CarService.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public OrderService(UserManager<User> userManager, IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        public async Task CreateOrder(string email, OrderCreationDto orderDto)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    IRepository<Auto> autos = unitOfWork.Repository<Auto>();
                    var auto = autos.Find(x => x.AutoRiaId == orderDto.AutoRiaId);
                    if (auto == null)
                    {
                        auto = new Auto
                        {
                            AutoRiaId = orderDto.AutoRiaId,
                            MarkName = orderDto.MarkName,
                            ModelName = orderDto.ModelName,
                            Year = orderDto.Year,
                            City = orderDto.City,
                            PhotoLink = orderDto.PhotoLink
                        };
                        autos.Add(auto);
                    }

                    IRepository<Order> order = unitOfWork.Repository<Order>();
                    order.Add(new Order
                    {
                        CustomerId = user.Id,
                        AutoId = auto.Id,
                        Description = orderDto.Description,
                        Date = DateTime.Now.ToUniversalTime()
                    });

                    unitOfWork.Save();
                }
            }
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var orderRepository = unitOfWork.Repository<Order>();
                var autoRepository = unitOfWork.Repository<Auto>();
                return await orderRepository.Query().Join(autoRepository.Query(), x => x.AutoId, y => y.Id, (x, y) => y.City).Distinct().ToListAsync();
            }
        }

        public async Task<IEnumerable<BaseOrderInfo>> GetOrdersAsync(OrderSearchModel orderSearchModel, int skip, int take)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var orders = unitOfWork.Repository<Order>().Query();
                var autos = unitOfWork.Repository<Auto>().Query();
                var customers = unitOfWork.Repository<Customer>().Query();

                var query = from o in orders
                            join a in autos on o.AutoId equals a.Id
                            join c in customers on o.CustomerId equals c.Id
                            where (orderSearchModel.TypeId > 0 ? a.TypeId == orderSearchModel.TypeId : true)
                                && (orderSearchModel.MarkId > 0 ? a.MarkId == orderSearchModel.MarkId : true)
                                && (orderSearchModel.ModelId > 0 ? a.ModelId == orderSearchModel.ModelId : true)
                                && (!string.IsNullOrWhiteSpace(orderSearchModel.City) ? a.City == orderSearchModel.City : true)
                                && (orderSearchModel.MinYear > 0 ? a.Year >= orderSearchModel.MinYear : true)
                                && (orderSearchModel.MaxYear > 0 ? a.Year <= orderSearchModel.MaxYear : true)
                            select new BaseOrderInfo
                            {
                                OrderId = o.Id,
                                CustomerId = c.Id,
                                CustomerFirstName = c.FirstName,
                                CustomerLastName = c.LastName,
                                AutoId = a.Id,
                                MarkName = a.MarkName,
                                ModelName = a.ModelName,
                                CarPhotoUrl = a.PhotoLink
                            };

                return await query.Skip(skip).Take(take).ToListAsync();
            }
        }
    }
}