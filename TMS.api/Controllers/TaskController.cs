using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TMS.app.Dtos;
using TMS.app.Services;

namespace TMS.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;
        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _service.GetAllTasks();
            return Ok(tasks);
        }
        //[Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _service.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTask = await _service.CreateTask(createTaskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }
        //[Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedTask = await _service.UpdateTask(id, updateTaskDto);
            if (updatedTask == null) return NotFound();
            return Ok(updatedTask);
        }
        //[Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var result = await _service.DeleteTask(id);
            if (!result) return NotFound();
            return NoContent();
        }
        [HttpPatch("{Id:int}")]
        public async Task<ActionResult> UpdatePartially(int Id, JsonPatchDocument<UpdateTaskDto> TaskPatch)
        {
            if (TaskPatch == null)
                return BadRequest();
            var task = await _service.GetTaskById(Id);
            if (task == null)
            {
                return NotFound($"Task with Id {Id} was not found");
            }
            var TaskToPatch = new UpdateTaskDto
            {
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };
            TaskPatch.ApplyTo(TaskToPatch, ModelState);
            var UpdatedTask = await _service.UpdateTask(Id, TaskToPatch);
            return Ok(UpdatedTask);
        }
    }
}
