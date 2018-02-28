using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models.DTO;

namespace CarService.Api.Services
{
    public interface IProfileService
    {
        Task EditCustomerProfile(CustomerDTO customerDTO);
        Task EditMechanicProfile(MechanicDTO mechanicDTO);
        Task<UserDTO> GetUserDTO(string email);
        Task AddCarToFavorites(string email, FavoritesDto body);
        Task<IEnumerable<ProfileOrderInfo>> GetUserCreatedOrders(string email);
        Task<IEnumerable<ProfileOrderInfo>> GetUserAppliedOrders(string email);
    }
}