using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CommunityPlant.Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreatePlant([FromBody] CreatePlantDTO plantDto)
        {
            try
            {
                var result = await _plantService.CreatePlantAsync(plantDto);
                return CreatedAtAction(nameof(GetPlantById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlantById(int id)
        {
            var result = await _plantService.GetPlantByIdAsync(id);
            if (result == null)
                return NotFound("Planta não encontrada.");
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlants()
        {
            var result = await _plantService.GetAllPlantsAsync();
            return Ok(result);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetPlantsByType(string type)
        {
            try
            {
                var result = await _plantService.GetPlantsByTypeAsync(type);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchPlants(string searchTerm)
        {
            try
            {
                var result = await _plantService.SearchPlantsAsync(searchTerm);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, [FromBody] PlantDTO plantDto)
        {
            try
            {
                var result = await _plantService.UpdatePlantAsync(id, plantDto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var result = await _plantService.DeletePlantAsync(id);
            if (!result)
                return NotFound("Planta não encontrada.");
            
            return NoContent();
        }
    }
}
