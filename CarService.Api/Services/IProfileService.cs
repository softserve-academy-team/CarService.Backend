using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;
using CarService.Api.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace CarService.Api.Services
{
    public interface IProfileService
    {
        Task EditCustomerProfile(string email, CustomerDto customerDTO);
        Task EditMechanicProfile(string email, MechanicDto mechanicDTO);
        Task<Models.DTO.UserDto> GetUserDTO(string email);
        Task<IEnumerable<ProfileOrderInfo>> GetUserCreatedOrders(string email);
        Task<IEnumerable<ProfileOrderInfo>> GetUserAppliedOrders(string email);
        Task<IEnumerable<ProfileReviewInfo>> GetUserBoughtReviews(string email);
        Task<IEnumerable<ProfileReviewInfo>> GetUserCreatedReviews(string email);
        Task AddCarToFavorites(string email, int autoRiaId);
        Task DeleteCarFromFavorites(string email, int autoRiaId);
        Task<IEnumerable<BaseCarInfo>> GetAllCarsFromFavorites(string email);
        Task<bool> IsCarInFavorites(string email, int autoRiaId);
        Task UploadAvatar(IFormFile photo, string email);
        Task<string> GetAvatarUrl(string email);
    }
}