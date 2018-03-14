using System.Threading.Tasks;
using CarService.Api.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace CarService.Api.Services
{
    public interface IReviewService
    {
        Task<int> CreateReview(ReviewCreationDto reviewCreationDto, string email);
        Task UploadPhoto(IFormFile photo, int reviewId, string email);
        Task UploadVideo(IFormFile video, int reviewId, string email);
    }
}