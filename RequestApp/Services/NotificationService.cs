using AutoMapper;
using Dto.ViewModels;
using Repositories.IRepositories;
using RequestApp.Filter;

namespace RequestApp.Services
{
    public class NotificationService
    {
        private readonly IRepositoryWrapper _dbContext;
        private readonly IMapper _mapper;

        public NotificationService(IRepositoryWrapper dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<NotificationViewModel> GetUserNotifications(string userId, PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var notifications = _dbContext.NotificationRepo.GetAllAsync(n => n.UserId == userId).Result
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            return _mapper.Map<List<NotificationViewModel>>(notifications);
        }
        public void DeleteNotifications(int Id)
        {
            _dbContext.NotificationRepo.Remove(Id);
        }
    }
}
