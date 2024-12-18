using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.core.Dtos.ProjectDto;
using TaskManagement.core.InterfaceServices;
using TaskManagement.services;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        // used repository py UOW
        private readonly IProjectServices _ProjectServices;
        public ProjectController(IProjectServices ProjectServices)
        {
            _ProjectServices = ProjectServices;
        }

        [HttpGet("GetAllProject")]
        
        public IActionResult GetProjects()
        {
            return Ok(_ProjectServices.GetProjects());
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetProject(int id)
        {
            var project= _ProjectServices.GetProject(id);
            if (project == null) return NotFound("not found id");
            return Ok(project);
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjctDto projectDto)
        {
            return Ok(_ProjectServices.CreateProject(projectDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project=_ProjectServices.DeleteProject(id);
            if (project == null) return NotFound("not found project");
            return Ok(project);

        }

        [HttpGet("{projectId}/tasks/user/{userId}")]
        public ActionResult GetTasksForUserInProject(int projectId, int userId)
        {
            var tasks= _ProjectServices.GetTasksForUserInProject(projectId, userId);
            if(tasks == null) return NotFound("not found");
            return Ok(tasks);
        }

        [HttpGet("withTasks")]
        public ActionResult GetProjectsWithTasks()
        {
            var project=_ProjectServices.GetProjectsWithTasks();
            if (project == null) return NotFound("not found");
            return Ok(project);
        }
    
    }
}

//not used repository
/*
     private readonly ApplicationDbContext _context;

     public ProjectController(ApplicationDbContext context)
     {
         _context = context;
     }

     [HttpGet("GetAllProject")]
     public async Task<IActionResult> GetProjects()
     {
         var projects= await _context.Projects
             .Include(p=>p.task)
             .ToListAsync();

         var readProject =projects.Select(project=>new ReadProjectDto
         {
             Id = project.Id,
             Name = project.Name,
             Description = project.Description,
             task=project.task.Select(task=>new ReadTaskDto 
             {
                 Id = task.Id,
                 Description = task.Description,
                 DueDate = task.DueDate,
                 Priority = task.Priority,
                 ProjectId = project.Id,
                 Status = task.Status,
                 Title = task.Title,
                 UserId = task.UserId
             }).ToList()

         }).ToList();
         return Ok(readProject);
     }


     [HttpGet("{id}")]
     public async Task<IActionResult> GetProject(int id)
     {
         var project = await _context.Projects.Include(p=>p.task).SingleOrDefaultAsync(p=>p.Id==id);

         if (project == null) return NotFound($"Not found id {id}");

             var ProjectDto = new ReadProjectDto
             {
                 Id = project.Id,
                 Name = project.Name,
                 Description = project.Description,
                 task = project.task.Select(task => new ReadTaskDto
                 {
                     Id = task.Id,
                     Description = task.Description,
                     DueDate = task.DueDate,
                     Priority = task.Priority,
                     ProjectId = project.Id,
                     Status = task.Status,
                     Title = task.Title,
                     UserId = task.UserId
                 }).ToList()
             };
             return Ok(ProjectDto);
         }


     [HttpPost]
     public async Task<IActionResult> PostProject(ProjectDto ProjectDto)
     {
         var project = new Projecty
         {
             Name = ProjectDto.Name,
             Description = ProjectDto.Description
         };
         _context.Projects.Add(project);
         await _context.SaveChangesAsync();
         var readProject = new ReadProjectDto
         {
             Id = project.Id,
             Name = project.Name,
             Description = project.Description,
             task = null
         };
         return Ok(project);
     }


     [HttpPut("{id}")]
     public async Task<IActionResult> PutProject(int id, UpdateProjctDto p)
     {
         var project = await _context.Projects.Include(p => p.task).SingleOrDefaultAsync(p => p.Id == id);

         if (project == null) return NotFound($"Not found id {id}");

         project.Description = p.Description;
         project.Name = p.Name;
         await _context.SaveChangesAsync();

         var ProjectDto = new ReadProjectDto
         {
             Id = project.Id,
             Name = project.Name,
             Description = project.Description,
             task = project.task.Select(task => new ReadTaskDto
             {
                 Id = task.Id,
                 Description = task.Description,
                 DueDate = task.DueDate,
                 Priority = task.Priority,
                 ProjectId = project.Id,
                 Status = task.Status,
                 Title = task.Title,
                 UserId = task.UserId
             }).ToList()
         };
         return Ok(ProjectDto);

     }


     [HttpDelete("{id}")]
     public async Task<IActionResult> DeleteProject(int id)
     {
         var project = await _context.Projects.FindAsync(id);
         if (project == null) return NotFound($"Not found id {id}");



         _context.Projects.Remove(project);
         await _context.SaveChangesAsync();
         var ProjectDto = new ReadProjectDto();
         if (project.task != null)
         {
             ProjectDto = new ReadProjectDto
             {
                 Id = project.Id,
                 Name = project.Name,
                 Description = project.Description,
                 task = project.task.Select(task => new ReadTaskDto
                 {
                     Id = task.Id,
                     Description = task.Description,
                     DueDate = task.DueDate,
                     Priority = task.Priority,
                     ProjectId = project.Id,
                     Status = task.Status,
                     Title = task.Title,
                     UserId = task.UserId
                 }).ToList()
             };
         }
         else
         {
             ProjectDto = new ReadProjectDto
             {
                 Id = project.Id,
                 Name = project.Name,
                 Description = project.Description,

             };
         }

         return Ok(ProjectDto);

     }


     [HttpGet("{projectId}/tasks/user/{userId}")]
     public async Task<ActionResult<IEnumerable<Task>>> GetTasksForUserInProject(int projectId, int userId)
     {
         var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

         if (project == null)  return NotFound($"Not found id {projectId}");

         var tasks = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId);

         if (tasks == null ) return NotFound($"Not found id {userId}");

         var task = new ReadTaskDto
         {
             Id = tasks.Id,
             Title = tasks.Title,
             Description = tasks.Description,
             DueDate = tasks.DueDate,
             Priority = tasks.Priority,
             Status = tasks.Status,
             UserId = tasks.UserId,
             ProjectId = tasks.ProjectId
         };

         return Ok(task);
     }


     [HttpGet("withTasks")]
      public async Task<ActionResult<IEnumerable<Projecty>>> GetProjectsWithTasks()
      {
          var projects = await _context.Projects.Include(p => p.task).ToListAsync();

          var readProject = projects.Select(project => new ReadProjectDto
          {
              Id = project.Id,
              Name = project.Name,
              Description = project.Description,
              task = project.task.Select(task => new ReadTaskDto
              {
                  Id = task.Id,
                  Description = task.Description,
                  DueDate = task.DueDate,
                  Priority = task.Priority,
                  ProjectId = project.Id,
                  Status = task.Status,
                  Title = task.Title,
                  UserId = task.UserId
              }).ToList()

          }).ToList();
          return Ok(readProject);
      }*/