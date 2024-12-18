using Microsoft.AspNetCore.Mvc;
using TaskManagement.core.Dtos.TaskDto;
using TaskManagement.core.InterfaceServices;



namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // used repository py UOW

        private readonly ITaskServices _taskServices;
        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [HttpGet("GetAllTasks")]
        public IActionResult GetTasks()
        {
            return Ok(_taskServices.GetTasks());
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
                var task = _taskServices.GetTask(id);
                return Ok(task);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetTasksByUserId(int userId)
        {
            var tasks=_taskServices.GetTasksByUserId(userId);
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult addTask(CreateTaskDto task )
        {
            var readtask = _taskServices.AddTask(task);
            return Ok(readtask);
        }

        [HttpPut("{id}")]
        public IActionResult updateTask(int id, UpdateTaskDto task)
        {

            var readtask = _taskServices.updateTask(id, task);
            return Ok(readtask);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task=_taskServices.DeleteTask(id);
            return Ok(task);
        }

        [HttpPatch("{id}/status")]
        public IActionResult UpdateTaskStatus(int id, [FromBody] string status)
        {
            var task = _taskServices.UpdateTaskStatus(id,status);
            return Ok(task);
        }
    }
}

// not used repository
/*
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var taskDtos = tasks.Select(task => new ReadTaskDto
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
            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.Include(u=>u.Usery).Include(p=>p.Projecty).FirstOrDefaultAsync(t=>t.Id==id);

            if (task == null) return NotFound($"Not found id {id}");

            var raesTask = new ReadTaskDto
            {
                Id = task.Id,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                ProjectId = task.ProjectId,
                UserId = task.UserId,
            };

            return Ok(raesTask);
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

            if (tasks == null || tasks.Count == 0) return NotFound($"Not found user id {userId}");
            var taskDtos = tasks.Select(task => new ReadTaskDto
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

            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<IActionResult> PostTask(CreateTaskDto t)
        {
            var userId= await _context.Users.FindAsync(t.UserId);
            if (userId == null) return NotFound($"Not found user id {t.UserId}");
            var projectId=await _context.Projects.FindAsync(t.ProjectId);
            if (projectId == null) return NotFound($"Not found project id {t.ProjectId}");

            var task=new Task 
            { 
                Description=t.Description,
                DueDate=t.DueDate,
                Priority = t.Priority,
                Status = t.Status,
                Title = t.Title,
                ProjectId = t.ProjectId,
                UserId=t.UserId, 
            };
           _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            var readTask = new ReadTaskDto
            {
                Id=task.Id,
                ProjectId=task.ProjectId,
                Description=task.Description,
                DueDate=task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                UserId = task.UserId,
            };

            return Ok(readTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id,UpdateTaskDto t)
        {
            var task=await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound($"Not found Id{id}");

            var userId = await _context.Users.FindAsync(t.UserId);
            if (userId == null) return NotFound($"Not found user id {t.UserId}");

            var projectId = await _context.Projects.FindAsync(t.ProjectId);
            if (projectId == null) return NotFound($"Not found project id {t.ProjectId}");

            task.Status = t.Status;
            task.Title = t.Title;
            task.ProjectId = t.ProjectId;
            task.Description = t.Description;
            task.DueDate = t.DueDate;
            task.Priority = t.Priority;
            task.UserId = t.UserId;

            await _context.SaveChangesAsync();

            var readTask = new ReadTaskDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                UserId = task.UserId,
            };


            return Ok(readTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound($"No found id {id}");
            
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            var readTask = new ReadTaskDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                UserId = task.UserId,
            };

            return Ok(readTask);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] string status)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound($"No found id {id}");



            task.Status = status;
            await _context.SaveChangesAsync();

            var readTask = new ReadTaskDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                Title = task.Title,
                UserId = task.UserId,
            };

            return Ok(readTask);
        }*/