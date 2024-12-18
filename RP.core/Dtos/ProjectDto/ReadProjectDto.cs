using TaskManagement.core.Dtos.TaskDto;

namespace TaskManagement.core.Dtos.ProjectDto
{
    public class ReadProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public List<ReadTaskDto> task { get; set; }
    }
}
