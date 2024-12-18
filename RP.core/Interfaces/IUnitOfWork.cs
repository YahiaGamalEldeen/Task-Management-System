using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Projecty> Projects { get; }
        IBaseRepository<Task> Tasks { get; }
        IBaseRepository<Usery> Users { get; }

        int Complete();
    }
}