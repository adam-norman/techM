using Domain.Models;
using Persistance;
using Repositories.IRepositories;

namespace Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        protected readonly AppDbContext _repositoryContext;
        public NotificationRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
    }
}
