using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.app.Dtos;

namespace TMS.app.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasks();
        Task<TaskDto?> GetTaskById(int id);
        Task<TaskDto> CreateTask(CreateTaskDto createTaskDto);
        Task<TaskDto?> UpdateTask(int id, UpdateTaskDto updateTaskDto);
        Task<bool> DeleteTask(int id);
    }
}
