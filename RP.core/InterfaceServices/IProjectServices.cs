using TaskManagement.core.Dtos.ProjectDto;
using TaskManagement.core.Dtos.TaskDto;
using TaskManagement.core.ResponseModel;

namespace TaskManagement.core.InterfaceServices
{
    public interface IProjectServices
    {
        ResponseModel<IEnumerable<ReadProjectDto>> GetProjects(); 
        ResponseModel<ReadProjectDto> GetProject(int id);
        ResponseModel<ReadProjectDto> CreateProject(CreateProjctDto projectDto);
        ResponseModel<ReadProjectDto> DeleteProject(int id);
        ResponseModel<IEnumerable<ReadTaskDto>> GetTasksForUserInProject(int projectId, int userId);
        ResponseModel<IEnumerable<ReadProjectDto>> GetProjectsWithTasks();
    }
}
