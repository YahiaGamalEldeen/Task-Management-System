using TaskManagement.core.Dtos.TaskDto;
namespace TaskManagement.core.Dtos.UserDto
{
     public class ReadUserDto
     {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public List<ReadTaskDto>? task { get; set; }
     }
}
