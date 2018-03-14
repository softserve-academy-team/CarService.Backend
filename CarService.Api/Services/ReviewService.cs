using System;
using System.IO;
using System.Threading.Tasks;
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

                    string url = await _azureBlobStorage.UploadFile(stream, $"review_{reviewId}", photo.FileName);

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

                    string url = await _azureBlobStorage.UploadFile(stream, $"review_{reviewId}", video.FileName);

                    videos.Add(new Video { ReviewId = reviewId, Url = url });
                }

                await unitOfWork.SaveAsync();
            }
        }
    }
}