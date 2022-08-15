using Domain.Models;
using MediatR;
using Repositories.IRepositories;

namespace Application.Commands
{
    public class SendProcessedNotificationCommandHandler : IRequestHandler<SendNotificationCommand, bool>
    {
        private readonly IRepositoryWrapper _dbContext;

        public SendProcessedNotificationCommandHandler(IRepositoryWrapper dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification() { Message = request.Message, Opened = request.Opened, UserId = request.UserId };
            await _dbContext.NotificationRepo.AddAsync(notification);
            _dbContext.Save();
            return true;
        }
    }
}
