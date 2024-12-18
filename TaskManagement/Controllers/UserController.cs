using Microsoft.AspNetCore.Mvc;
using TaskManagement.core.Dtos.UserDto;
using TaskManagement.core.InterfaceServices;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        // used repository UOW py UOW
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id) 
        {
            var user =_userServices.GetUser(id);
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_userServices.GetUsers());
        }

        [HttpPost("Create")]
        public IActionResult PostUser(CreateUserDto user)
        {
            return Ok(_userServices.CreateUser(user));
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, UpdateUserDto user)
        {
            var uesr=_userServices.UpdateUser(id, user);
           
            return Ok(uesr);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var uesr = _userServices.DeleteUser(id);
            
            return Ok(uesr);
        }

        [HttpPatch("{id}/email")]
        public IActionResult UpdateUserEmail(int id, [FromBody] string email)
        {
            var uesr = _userServices.UpdateUserEmail(id,email);
            
            return Ok(uesr);
        }
        
        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userServices.GetUserByEmail(email);
            
            return Ok(user);
        }

    }
}
// not used repository
/*
 
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {

            var users = await _context.Users
            .Include(u => u.task) 
            .ToListAsync();

        var userDtos = users.Select(user => new ReadUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            task = user.task.Select(t => new ReadTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Priority = t.Priority,
                Status = t.Status
            }).ToList()
        }).ToList();

        return Ok(userDtos);


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.Include(t=>t.task).SingleOrDefaultAsync(t=>t.Id==id);
            if (user == null) return NotFound($"id {id} is not found");
            var userDto1 = new ReadUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                    task = user.task.Select(t => new ReadTaskDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        DueDate = t.DueDate,
                        Priority = t.Priority,
                        Status = t.Status
                    }).ToList()
            };  

            return Ok(userDto1);
        }

       [HttpPost("Create")]

        public async Task<IActionResult> PostUser(CreateUserDto user)
        {
             var u = new Usery { Name=user.Name, Email=user.Email,Password=user.Password}; 
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
            var readUser = new ReadUserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                task = null
            };
            return Ok(readUser);
        }

       [HttpPut("{id}")]
       public async Task<IActionResult> PutUser(int id, UpdateUserDto user)
       {
            var u=await _context.Users.FindAsync(id);
            if (u == null) return NotFound($"id {id} is not found");

            u.Name= user.Name;
            u.Email= user.Email;
            u.Password= user.Password;
            u.task = u.task;
            await _context.SaveChangesAsync();
            var userDto1 = new ReadUserDto();
            if (u.task != null)
            {
                userDto1 = new ReadUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    task = u.task.Select(t => new ReadTaskDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        DueDate = t.DueDate,
                        Priority = t.Priority,
                        Status = t.Status
                    }).ToList()
                };
            }
            else
            {
                userDto1 = new ReadUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = user.Password,
                    task=null
                };
            }

            return Ok(userDto1);
        }

       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteUser(int id)
       {
           var user = await _context.Users.FindAsync(id);
           if (user == null) return NotFound($"id {id} is not found");

           _context.Users.Remove(user);
           await _context.SaveChangesAsync();

           return Ok(user);
       }

       [HttpPatch("{id}/email")]
       public async Task<IActionResult> UpdateUserEmail(int id, [FromBody] string email)
       {
           var u = await _context.Users.FindAsync(id);
           if (u == null) return NotFound($"id {id} is not found");
           u.Email = email;
           await _context.SaveChangesAsync();
            var userDto1 = new ReadUserDto();
            if (u.task != null)
            {
                userDto1 = new ReadUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    task = u.task.Select(t => new ReadTaskDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        DueDate = t.DueDate,
                        Priority = t.Priority,
                        Status = t.Status
                    }).ToList()
                };
            }
            else
            {
                userDto1 = new ReadUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    task = null
                };
            }

            return Ok(userDto1);
        }

       [HttpGet("email/{email}")]
       public async Task<IActionResult> GetUserByEmail(string email)
       {
           var u = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
           if (u == null) return NotFound($"Email {email} is not found");
            var userDto1 = new ReadUserDto();
            if (u.task != null)
            {
                userDto1 = new ReadUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    task = u.task.Select(t => new ReadTaskDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        DueDate = t.DueDate,
                        Priority = t.Priority,
                        Status = t.Status
                    }).ToList()
                };
            }
            else
            {
                userDto1 = new ReadUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    task = null
                };
            }

            return Ok(userDto1);
        }
        */