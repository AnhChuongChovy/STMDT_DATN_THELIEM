using DATN_STMDT_THELIEMS.Models;
using System.Net;
using System.Threading.Tasks;

namespace DATN_STMDT_THELIEMS.Services
{
    public interface IUserService
    {
        Task<Users> GetUserByIdAsync(int id);
        Task<Users> UpdateUserByIdAsync(int id, Users users);
        Task<Users> FindOrCreateUserAsync(Users users);
        Task<Delivery_address> GetDefaultAddressByUserIdAsync(int id);
        Task<List<Orders>> GetOrdersByUserIdAsync(int userId);
        Task<bool> AddAddressAsync(Delivery_address address);
        Task<bool> UpdateAddressAsync(Delivery_address address);
    }
}