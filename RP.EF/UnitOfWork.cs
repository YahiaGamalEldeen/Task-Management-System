
using TaskManagement.core.Interfaces;
using TaskManagement.EF.Repositorys;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Projecty> Projects { get; private set; }
        public IBaseRepository<Task> Tasks { get; private set; }
        public IBaseRepository<Usery> Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Projects=new BaseRepository<Projecty>(_context);
            Tasks=new BaseRepository<Task>(_context);
            Users=new BaseRepository<Usery>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}