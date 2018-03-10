using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Api.Models;
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

        public async Task<CustomerOrderInfo> GetCustomerOrderInfo(string email, int orderId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var orderRepository = unitOfWork.Repository<Order>();
                var autoRepository = unitOfWork.Repository<Auto>();

                var order = await orderRepository.GetAsync(orderId);

                if (order == null || order.CustomerId != user.Id)
                    return null;

                var auto = await autoRepository.GetAsync((int)order.AutoId);

                var orderInfo = new CustomerOrderInfo()
                {
                    OrderId = order.Id,
                    Status = order.Status.ToString(),
                    Date = order.Date.ToString("dd-MM-yyyy"),
                    AutoRiaId = auto.AutoRiaId,
                    MarkName = auto.MarkName,
                    ModelName = auto.ModelName,
                    Year = auto.Year,
                    PhotoLink = auto.PhotoLink,
                    ReviewPropositions = await GetReviewPropositions(order.Id)
                };

                if (order.Status != OrderStatus.Canceled)
                    orderInfo.ReviewPropositions = await GetReviewPropositions(order.Id);

                if (order.Status == OrderStatus.Pending || order.Status == OrderStatus.Done)
                    orderInfo.MechanicId = (int)order.MechanicId;

                if (order.Status == OrderStatus.Done)
                    orderInfo.ReviewId = (int)order.ReviewId;

                return orderInfo;
            }
        }

        public async Task AcceptReviewProposition(string email, AcceptReviewProposition acceptReviewProposition)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return;

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var orderRepository = unitOfWork.Repository<Order>();
                var propositionRepository = unitOfWork.Repository<ReviewProposition>();

                var order = await orderRepository.GetAsync(acceptReviewProposition.OrderId);
                var proposition = await propositionRepository.GetAsync(acceptReviewProposition.ReviewPropositionId);

                if (order == null || proposition == null || order.CustomerId != user.Id || proposition.OrderId != order.Id)
                    return;

                order.Status = OrderStatus.Pending;
                order.MechanicId = proposition.MechanicId;

                orderRepository.Attach(order);
                await unitOfWork.SaveAsync();
            }
        }

        private async Task<IEnumerable<ReviewPropositionDto>> GetReviewPropositions(int orderId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var propositionRepository = unitOfWork.Repository<ReviewProposition>();
                var mechanicRepository = unitOfWork.Repository<Mechanic>();

                var propositions = from p in propositionRepository.Query()
                                   where p.OrderId == orderId
                                   select p;

                var propositionsList = new List<ReviewPropositionDto>();

                foreach (var proposion in propositions)
                {
                    var mechnic = await mechanicRepository.GetAsync((int)proposion.MechanicId);
                    propositionsList.Add(new ReviewPropositionDto
                    {
                        Id = proposion.Id,
                        MechanicId = (int)proposion.MechanicId,
                        FirstName = mechnic.FirstName,
                        LastName = mechnic.LastName,
                        MechanicRate = mechnic.MechanicRate,
                        Price = proposion.Price,
                        Comment = proposion.Comment,
                        Date = proposion.Date.ToString("dd-MM-yyyy")
                    });
                }

                return propositionsList;
            }
        }
    }
}