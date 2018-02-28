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
    }
}