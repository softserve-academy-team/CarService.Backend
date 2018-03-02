
using System;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Api.Models.DTO;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}