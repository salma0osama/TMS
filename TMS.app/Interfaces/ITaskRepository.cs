using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.app.Dtos;
using TMS.core.Entities;

namespace TMS.app.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasks();
        Task<TaskItem?> GetTaskById(int id);
        Task<TaskItem?> CreateTask(TaskItem taskItem);
        Task<TaskItem?> UpdateTask(int id, TaskItem taskItem);
        Task<bool> DeleteTask(int id);
    }
}
