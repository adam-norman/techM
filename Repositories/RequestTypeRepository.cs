using Domain.Models;
using Persistance;
using Repositories.IRepositories;

namespace Repositories
{
    public class RequestTypeRepository : Repository<RequestType>, IRequestTypeRepository
    {
        protected readonly AppDbContext _repositoryContext;
        public RequestTypeRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
    }
}
