
using Mapster;
using TaskManagement.core.Dtos.ProjectDto;
using TaskManagement.core.Dtos.TaskDto;
using TaskManagement.core.Interfaces;
using TaskManagement.core.InterfaceServices;
using TaskManagement.core.ResponseModel;
using TaskManagement.Models;

namespace TaskManagement.services
{
    public class ProjectServices : IProjectServices
    {
        private readonly IUnitOfWork _uniOfWork;
        public ProjectServices(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }

        public ResponseModel<IEnumerable<ReadProjectDto>> GetProjects() 
        {
            var projects = _uniOfWork.Projects.GetAll(new[] { "task" });
            var readProject = projects.Adapt<IEnumerable<ReadProjectDto>>();
            return ResponseModel<IEnumerable<ReadProjectDto>>.Success(readProject);
        }

        public ResponseModel<ReadProjectDto> GetProject(int id) 
        {
            var project = _uniOfWork.Projects.find(p => p.Id == id, new[] { "task" });
            if (project == null) return ResponseModel<ReadProjectDto>.Error("Not found Id");
            var projectDto = project.Adapt<ReadProjectDto>();
            return ResponseModel<ReadProjectDto>.Success(projectDto);
        }

        public ResponseModel<ReadProjectDto> CreateProject(CreateProjctDto projectDto)
        {
            try
            {
                var project = projectDto.Adapt<Projecty>();
                _uniOfWork.Projects.add(project);
                _uniOfWork.Complete();
                var readProject = project.Adapt<ReadProjectDto>();
                return ResponseModel<ReadProjectDto>.Success(readProject);
            }
            catch 
            {
                return ResponseModel<ReadProjectDto>.Error("Error");
            }
        }

        public ResponseModel<ReadProjectDto> DeleteProject(int id)
        {
            var project = _uniOfWork.Projects.find(p => p.Id == id);
            if (project == null) return ResponseModel<ReadProjectDto>.Error("Not found Id");
            try { 
            _uniOfWork.Projects.delete(project);
            _uniOfWork.Complete();
            var projectDto = project.Adapt<ReadProjectDto>();
            return ResponseModel<ReadProjectDto>.Success(projectDto);
        }
        catch 
        {
            return ResponseModel<ReadProjectDto>.Error("Error");
        }
}

        public ResponseModel<IEnumerable<ReadTaskDto>> GetTasksForUserInProject(int projectId, int userId)
        {
            var tasks = _uniOfWork.Tasks.GetAllbyExpression(u => u.UserId == userId,
                                                            p => p.ProjectId == projectId);
            if (tasks == null) return ResponseModel<IEnumerable<ReadTaskDto>>.Error("Not found Id");
            var tasksDto=tasks.Adapt<IEnumerable< ReadTaskDto>>();
            return ResponseModel<IEnumerable<ReadTaskDto>>.Success(tasksDto);
        }
         
        public ResponseModel<IEnumerable<ReadProjectDto>> GetProjectsWithTasks() 
        {
            var projects = _uniOfWork.Projects.GetAllbyExpression(t => t.task.Any(), new[] { "task" });
            if (projects == null) return ResponseModel<IEnumerable<ReadProjectDto>>.Error("Not found Id");
            var readProject = projects.Adapt<IEnumerable<ReadProjectDto>>();
            return ResponseModel<IEnumerable<ReadProjectDto>>.Success(readProject);
        }
    }
}
