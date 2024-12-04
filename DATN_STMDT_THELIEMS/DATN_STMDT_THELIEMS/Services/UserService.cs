using DATN_STMDT_THELIEMS.DATA;
using DATN_STMDT_THELIEMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace DATN_STMDT_THELIEMS.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;

        public UserService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _context.USERS
                .Include(p => p.Role)
                .Include(p => p.Orders)
                .ThenInclude(p => p.Order_Details)
                .ThenInclude(p => p.Product_variants)
                .Include(p => p.Delivery_Addresses)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Users> UpdateUserByIdAsync(int id, Users users)
        {
            var existtingUses = await _context.USERS.FirstOrDefaultAsync(u => u.Id == users.Id && u.Status != 0);
            if (existtingUses == null)
            {
                return null;
            }
            existtingUses.Full_name = users.Full_name ?? existtingUses.Full_name;
            existtingUses.Email = users.Email ?? existtingUses.Email;
            existtingUses.Phone = users.Phone ?? existtingUses.Phone;
            existtingUses.Birthday = users.Birthday ?? existtingUses.Birthday;
            existtingUses.Password = users.Password ?? existtingUses.Password;
            existtingUses.Image = users.Image ?? existtingUses.Image;
            existtingUses.Gender = users.Gender ?? existtingUses.Gender;

            _context.USERS.Update(existtingUses);
            await _context.SaveChangesAsync();

            return existtingUses;
        }
        public async Task<Users> FindOrCreateUserAsync(Users user)
        {
            // Kiểm tra người dùng đã tồn tại hay chưa dựa trên email
            var existingUser = await _context.USERS
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                // Nếu đã tồn tại, trả về người dùng
                return existingUser;
            }

            // Nếu không tồn tại, tạo người dùng mới
            user.Created_at = DateTime.UtcNow;
            user.Updated_at = DateTime.UtcNow;
            user.Status = 1; // Kích hoạt người dùng mặc định
            user.Password = null; // Không lưu mật khẩu cho người dùng Facebook

            // Thêm vào cơ sở dữ liệu
            _context.USERS.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Delivery_address> GetDefaultAddressByUserIdAsync(int id)
        {
            return await _context.DELIVERY_ADDRESSES
                .FirstOrDefaultAsync(a => a.User_id == id);
        }

        public async Task<List<Orders>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.ORDERS
                .Where(o => o.User_id == userId) // Lọc theo UserId
                .Include(o => o.Order_Details) // Bao gồm chi tiết đơn hàng
                    .ThenInclude(od => od.Product_variants) // Bao gồm biến thể sản phẩm
                        .ThenInclude(pv => pv.Products) // Bao gồm sản phẩm
                .ToListAsync(); // Lấy danh sách
        }

        public async Task<bool> AddAddressAsync(Delivery_address address)
        {
            try
            {
                _context.DELIVERY_ADDRESSES.Add(address);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAddressAsync(Delivery_address address)
        {
            try
            {
                _context.DELIVERY_ADDRESSES.Update(address);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
