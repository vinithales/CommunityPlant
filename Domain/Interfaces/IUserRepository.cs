using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
    }
}