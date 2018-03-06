using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;
using CarService.Api.Models.DTO;

namespace CarService.Api.Services
{
    public interface IProfileService
    {
        Task EditCustomerProfile(string email, CustomerDTO customerDTO);
        Task EditMechanicProfile(string email, MechanicDTO mechanicDTO);
        Task<Models.DTO.UserDTO> GetUserDTO(string email);
        Task<IEnumerable<ProfileOrderInfo>> GetUserCreatedOrders(string email);
        Task<IEnumerable<ProfileOrderInfo>> GetUserAppliedOrders(string email);
        Task AddCarToFavorites(string email, int autoRiaId);
        Task DeleteCarFromFavorites(string email, int autoRiaId);
        Task<IEnumerable<BaseCarInfo>> GetAllCarsFromFavorites(string email);
        Task<bool> IsCarInFavorites(string email, int autoRiaId);
    }
}