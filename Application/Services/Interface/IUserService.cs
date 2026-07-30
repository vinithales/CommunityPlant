using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Domain.Entities;

namespace CommunityPlant.Application.Services.Interface
{
    public interface IUserService
    {
        Task<UserResponseDTO> CreateUserAsync(CreateUserDTO userDto);
        Task<UserResponseDTO?> GetUserByIdAsync(int id);
        Task<UserResponseDTO?> GetUserByEmailAsync(string email);
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO> UpdateUserAsync(int id, UserDTO userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task<User?> AuthenticateAsync(string email, string password);
    }
}
