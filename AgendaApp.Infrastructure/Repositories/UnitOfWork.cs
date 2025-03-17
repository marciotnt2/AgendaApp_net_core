using AgendaApp.Domain.Abstractions;
using AgendaApp.Infrastructure.Context;

namespace AgendaApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IContactRepository? _contactRepository;
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IContactRepository ContactRepository
        {
            get
            {
                return _contactRepository = _contactRepository ??
                  new ContactRepository(_appDbContext);
            }
        }
        public  async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
