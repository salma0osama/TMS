using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.app.Dtos;
using TMS.app.Interfaces;
using TMS.core.Entities;

namespace TMS.app.Services
{
    public class TaskServices : ITaskService
    {
        private readonly ITaskRepository _repository;
        public TaskServices(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskDto> CreateTask(CreateTaskDto createTaskDto)
        {
            var taskItem = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
            };
            var createdTask = await _repository.CreateTask(taskItem);
            return MapToDto(createdTask);
        }

        public async Task<bool> DeleteTask(int id)
        {
            return await _repository.DeleteTask(id);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasks()
        {
            var tasks = await _repository.GetAllTasks();
            return tasks.Select(MapToDto);
        }

        public async Task<TaskDto?> GetTaskById(int id)
        {
            var task = await _repository.GetTaskById(id);
            return task != null ? MapToDto(task) : null;
        }

        public async Task<TaskDto?> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            var existingTask = await _repository.GetTaskById(id);
            if (existingTask == null) return null;

            if (updateTaskDto.Title != null)
                existingTask.Title = updateTaskDto.Title;

            if (updateTaskDto.Description != null)
                existingTask.Description = updateTaskDto.Description;

            if (updateTaskDto.IsCompleted.HasValue)
                existingTask.IsCompleted = updateTaskDto.IsCompleted.Value;

            var updatedTask = await _repository.UpdateTask(id, existingTask);
            return updatedTask != null ? MapToDto(updatedTask) : null;
        }


        public static TaskDto MapToDto(TaskItem taskItem)
        {
            return new TaskDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted
            };
        }

    }
}
