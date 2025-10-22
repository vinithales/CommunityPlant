using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPlant.Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDto)
        {
            try
            {
                var result = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result == null)
                return NotFound("Usuário não encontrado.");
            
            return Ok(result);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            if (result == null)
                return NotFound("Usuário não encontrado.");
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, userDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound("Usuário não encontrado.");
            
            return NoContent();
        }

        [HttpPost("validate-credentials")]
        public async Task<IActionResult> ValidateCredentials([FromBody] LoginDTO loginDto)
        {
            var isValid = await _userService.ValidateUserCredentialsAsync(loginDto.Email, loginDto.Password);
            if (!isValid)
                return Unauthorized("Credenciais inválidas.");
            
            return Ok(new { Message = "Credenciais válidas." });
        }
    }

    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}