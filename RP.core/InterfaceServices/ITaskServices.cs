
using TaskManagement.core.Dtos.TaskDto;
using TaskManagement.core.ResponseModel;

namespace TaskManagement.core.InterfaceServices
{
    public interface ITaskServices
    {
        ResponseModel<IEnumerable<ReadTaskDto>> GetTasks();
        ResponseModel<ReadTaskDto> GetTask(int id);
        ResponseModel<IEnumerable<ReadTaskDto>> GetTasksByUserId(int userId);
        ResponseModel<ReadTaskDto> AddTask(CreateTaskDto t);
        ResponseModel<ReadTaskDto> updateTask(int id, UpdateTaskDto t);
        ResponseModel<ReadTaskDto> DeleteTask(int id);
        ResponseModel<ReadTaskDto> UpdateTaskStatus(int id, string status);
    }
}
