using Persistance;
using Repositories.IRepositories;

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _dbContext;
        public RepositoryWrapper(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            RequestFormRepo = new RequestFormRepository(dbContext);
            RequestTypeRepo = new RequestTypeRepository(dbContext);
            NotificationRepo = new NotificationRepository(dbContext);
            
        }
        public IRequestFormRepository RequestFormRepo
        {
            get;
            private set;
        }
        public INotificationRepository NotificationRepo
        {
            get;
            private set;
        }
        public IRequestTypeRepository RequestTypeRepo
        {
            get;
            private set;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
