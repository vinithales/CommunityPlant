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
    public class GardenController : ControllerBase
    {
        private readonly IGardenService _gardenService;

        public GardenController(IGardenService gardenService)
        {
            _gardenService = gardenService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGarden([FromBody] CreateGardenDTO gardenDto)
        {
            try
            {
                var result = await _gardenService.CreateGardenAsync(gardenDto);
                return CreatedAtAction(nameof(GetGardenById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGardenById(int id)
        {
            var result = await _gardenService.GetGardenByIdAsync(id);
            if (result == null)
                return NotFound("Jardim não encontrado.");
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGardens()
        {
            var result = await _gardenService.GetAllGardensAsync();
            return Ok(result);
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetPublicGardens()
        {
            var result = await _gardenService.GetPublicGardensAsync();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGardensByUser(int userId)
        {
            var result = await _gardenService.GetGardensByUserAsync(userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGarden(int id, [FromBody] GardenDTO gardenDto)
        {
            try
            {
                var result = await _gardenService.UpdateGardenAsync(id, gardenDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarden(int id)
        {
            var result = await _gardenService.DeleteGardenAsync(id);
            if (!result)
                return NotFound("Jardim não encontrado.");
            
            return NoContent();
        }
    }
}