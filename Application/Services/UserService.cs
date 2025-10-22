using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Interfaces;

namespace CommunityPlant.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(userDto.Email))
                throw new ArgumentException("Email é obrigatório.");

            if (await _userRepository.ExistsByEmailAsync(userDto.Email))
                throw new ArgumentException("Já existe um usuário com este email.");

            var user = _mapper.Map<User>(userDto);
            user.SetPassword(userDto.Password);

            var createdUser = await _userRepository.CreateUserAsync(user);
            return _mapper.Map<UserResponseDTO>(createdUser);
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user != null ? _mapper.Map<UserResponseDTO>(user) : null;
        }

        public async Task<UserResponseDTO?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user != null ? _mapper.Map<UserResponseDTO>(user) : null;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO> UpdateUserAsync(int id, UserDTO userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                throw new ArgumentException("Usuário não encontrado.");

            // Check if email is being changed and if it's already in use
            if (existingUser.Email != userDto.Email && await _userRepository.ExistsByEmailAsync(userDto.Email))
                throw new ArgumentException("Já existe um usuário com este email.");

            _mapper.Map(userDto, existingUser);
            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
            return _mapper.Map<UserResponseDTO>(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !user.IsActive)
                return false;

            return user.VerifyPassword(password);
        }
    }
}