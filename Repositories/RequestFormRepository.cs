using Domain.Models;
using Persistance;
using Repositories.IRepositories;

namespace Repositories
{
    public class RequestFormRepository : Repository<RequestForm>, IRequestFormRepository
    {
        protected readonly AppDbContext _repositoryContext;
        public RequestFormRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public Task UpdateRequest(RequestForm requestForm)
        {
            _repositoryContext.RequestForms.Update(requestForm);
            return Task.CompletedTask;
        }
        public Task UpdateRequestList(List<RequestForm> requestForms)
        {
            _repositoryContext.RequestForms.UpdateRange(requestForms);
            return Task.CompletedTask;
        }
    }
}
