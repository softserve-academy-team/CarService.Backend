using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using CarService.Api.Models.DTO;
using CarService.Api.Services.AzureServices;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace CarService.Api.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IAzureBlobStorageService _azureBlobStorage;

        public ReviewService(UserManager<User> userManager, IAzureBlobStorageService azureBlobStorage, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userManager = userManager;
            _azureBlobStorage = azureBlobStorage;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<int> CreateReview(ReviewCreationDto reviewCreationDto, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var reviews = unitOfWork.Repository<Review>();
                var orders = unitOfWork.Repository<Order>();

                var order = orders.Get(reviewCreationDto.OrderId);

                if (order == null || order.MechanicId != user.Id)
                    return 0;

                var review = new Review { Description = reviewCreationDto.Description, Date = DateTime.Now.ToUniversalTime(), OrderId = reviewCreationDto.OrderId, MechanicId = user.Id };

                reviews.Add(review);

                await unitOfWork.SaveAsync();

                order.ReviewId = review.Id;
                order.Status = OrderStatus.Done;

                await unitOfWork.SaveAsync();
                
                return review.Id;
            }
        }

        public async Task UploadPhoto(IFormFile photo, int reviewId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IRepository<Review> reviews = unitOfWork.Repository<Review>();
                IRepository<Photo> photos = unitOfWork.Repository<Photo>();

                var review = reviews.Get(reviewId);
                if (review == null)
                    return;

                if (review.MechanicId != user.Id)
                    return;

                using (var stream = new MemoryStream())
                {
                    await photo.CopyToAsync(stream);
                    stream.Position = 0;

                    string url = await _azureBlobStorage.UploadFile(stream, $"review{reviewId}", photo.FileName);

                    photos.Add(new Photo { ReviewId = reviewId, Url = url });
                }

                await unitOfWork.SaveAsync();
            }
        }

        public async Task UploadVideo(IFormFile video, int reviewId, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IRepository<Review> reviews = unitOfWork.Repository<Review>();
                IRepository<Video> videos = unitOfWork.Repository<Video>();

                var review = reviews.Get(reviewId);
                if (review == null)
                    return;

                if (review.MechanicId != user.Id)
                    return;

                using (var stream = new MemoryStream())
                {
                    await video.CopyToAsync(stream);
                    stream.Position = 0;

                    string url = await _azureBlobStorage.UploadFile(stream, $"review{reviewId}", video.FileName);

                    videos.Add(new Video { ReviewId = reviewId, Url = url });
                }

                await unitOfWork.SaveAsync();
            }
        }


        public async Task<ReviewDto> GetReview(string email, int orderId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var reviewRepository = unitOfWork.Repository<Review>();
                var photoRepository = unitOfWork.Repository<Photo>();
                var videoRepository = unitOfWork.Repository<Video>();
                var orderRepository = unitOfWork.Repository<Order>();
                var autoRepository = unitOfWork.Repository<Auto>();

                var order = await orderRepository.GetAsync(orderId);

                if (order == null || (order.CustomerId != user.Id && order.MechanicId != user.Id))
                    return null;

                var review = await reviewRepository.GetAsync((int)order.ReviewId);

                if (review == null)
                    return null;

                var auto = await autoRepository.GetAsync((int)order.AutoId);

                var photos = from p in photoRepository.Query()
                             where p.ReviewId == review.Id
                             select p.Url;

                var videos = from v in videoRepository.Query()
                             where v.ReviewId == review.Id
                             select v.Url;

                var reviewDto = new ReviewDto
                {
                    ReviewId = review.Id,
                    Description = review.Description,
                    Date = review.Date.ToString("dd-MM-yyyy"),
                    Photos = photos.ToList(),
                    Videos = videos.ToList(),
                    AutoRiaId = auto.AutoRiaId,
                    MarkName = auto.MarkName,
                    ModelName = auto.ModelName,
                    Year = auto.Year,
                    City = auto.City,
                    PhotoLink = auto.PhotoLink
                };

                return reviewDto;
            }
        }
    }
}