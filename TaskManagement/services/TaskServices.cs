using Mapster;
using TaskManagement.core.Dtos.TaskDto;
using TaskManagement.core.Interfaces;
using TaskManagement.core.InterfaceServices;
using TaskManagement.core.ResponseModel;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.services
{
    public class TaskServices: ITaskServices
    {
        private readonly IUnitOfWork _uniOfWork;
        public TaskServices(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }

        public ResponseModel<IEnumerable<ReadTaskDto>> GetTasks()
        {
            var tasks = _uniOfWork.Tasks.GetAll();

            var tasksDto = tasks.Adapt<IEnumerable<ReadTaskDto>>();

            return ResponseModel<IEnumerable<ReadTaskDto>>.Success(tasksDto); 
            /*
            var tasksDto = tasks.Select(task => new ReadTaskDto
            {
                Id = task.Id,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                ProjectId = task.ProjectId,
                UserId = task.UserId,
            });
            */
        }
        public ResponseModel<ReadTaskDto> GetTask(int id)
        {

            var task = _uniOfWork.Tasks.find(t => t.Id == id);

            if (task == null) return ResponseModel<ReadTaskDto>.Error("Not found Id");

            var readTask = task.Adapt<ReadTaskDto>();

            return ResponseModel<ReadTaskDto>.Success(readTask);

            /*
            var readTask = new ReadTaskDto
            {
                Id = task.Id,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                ProjectId = task.ProjectId,
                UserId = task.UserId,
            };*/
        }
        public ResponseModel<IEnumerable<ReadTaskDto>> GetTasksByUserId(int userId)
        {
            var tasks = _uniOfWork.Tasks.GetAllbyExpression(u => u.UserId == userId);

            if (tasks == null) return ResponseModel<IEnumerable<ReadTaskDto>>.Error("Not found Id");

            var tasksDto = tasks.Adapt<IEnumerable<ReadTaskDto>>();

            return ResponseModel<IEnumerable<ReadTaskDto>>.Success(tasksDto);
        }
        public ResponseModel<ReadTaskDto> AddTask(CreateTaskDto t)
        {

            var userId = _uniOfWork.Users.find(x => x.Id == t.UserId);
            if (userId == null) return ResponseModel<ReadTaskDto>.Error("user id not found");
            var projectId = _uniOfWork.Projects.find(x => x.Id == t.ProjectId);
            if (projectId == null) return ResponseModel<ReadTaskDto>.Error("project id not found");

            try 
            { 
                  var task = t.Adapt<Task>();
                  _uniOfWork.Tasks.add(task);
                  _uniOfWork.Complete();
                  var readTask = task.Adapt<ReadTaskDto>();
                  return ResponseModel<ReadTaskDto>.Success(readTask);
            }
            catch 
            {
                return ResponseModel<ReadTaskDto>.Error("Error");
            }
        }
        public ResponseModel<ReadTaskDto> updateTask(int id, UpdateTaskDto t)
        {
            var task = _uniOfWork.Tasks.find(x => x.Id == id);
            if (task == null) return ResponseModel<ReadTaskDto>.Error("task id not found");
            var userId = _uniOfWork.Users.find(x => x.Id == t.UserId);
            if (userId == null) return ResponseModel<ReadTaskDto>.Error("userId id not found");
            var projectId = _uniOfWork.Projects.find(x => x.Id == t.ProjectId);
            if (projectId == null) return ResponseModel<ReadTaskDto>.Error("projectId id not found");

            try
            {
                task.Status = t.Status;
                task.Title = t.Title;
                task.ProjectId = t.ProjectId;
                task.Description = t.Description;
                task.DueDate = t.DueDate;
                task.Priority = t.Priority;
                task.UserId = t.UserId;

                _uniOfWork.Tasks.update(task);
                _uniOfWork.Complete();
                var readTask = task.Adapt<ReadTaskDto>();
                return ResponseModel<ReadTaskDto>.Success(readTask);
            }
            catch 
            {
                return ResponseModel<ReadTaskDto>.Error("Error");
            }
        }
        public ResponseModel<ReadTaskDto> DeleteTask(int id)
        {
            var task = _uniOfWork.Tasks.find(x => x.Id == id);
            if (task == null) return ResponseModel<ReadTaskDto>.Error("task id not found");
            try { 
                _uniOfWork.Tasks.delete(task);
                _uniOfWork.Complete();
                var readTask = task.Adapt<ReadTaskDto>();
                return ResponseModel<ReadTaskDto>.Success(readTask);
            }
            catch
            {
                return ResponseModel<ReadTaskDto>.Error("Error");
            }
        }
        public ResponseModel<ReadTaskDto> UpdateTaskStatus(int id, string status)
        {
            var task = _uniOfWork.Tasks.find(x => x.Id == id);
            if (task == null) return ResponseModel<ReadTaskDto>.Error("task id not found");
            try
            {
                task.Status = status;
                _uniOfWork.Tasks.update(task);
                _uniOfWork.Complete();
                var readTask = task.Adapt<ReadTaskDto>();
                return ResponseModel<ReadTaskDto>.Success(readTask);
            }
            catch
            {
                return ResponseModel<ReadTaskDto>.Error("Error");
            }
        }
    }
}
