using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Application.DTOs;
using CommunityPlant.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPlant.Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;


        public TaskController(
            ITaskService taskService
        ){
            _taskService = taskService;
        }

         [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO taskDto)
        {
            var result = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { taskId = result.Id }, result);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var result = await _taskService.GetTaskByIdAsync(taskId);
            return Ok(result);
        }

        [HttpGet("garden/{gardenId}")]
        public async Task<IActionResult> GetTasksByGarden(int gardenId)
        {
            var result = await _taskService.GetTasksByGardenIdAsync(gardenId);
            return Ok(result);
        }

        [HttpPut("{taskId}/complete")]
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            await _taskService.CompleteTaskAsync(taskId);
            return NoContent();
        }
    }
}