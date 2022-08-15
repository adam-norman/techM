using Dto.ViewModels;
using MediatR;
using System.Runtime.Serialization;

namespace Application.Commands
{
    public class SendNotificationCommand : IRequest<bool>
    {
        [DataMember]
        public string Message { get; set; }
        public string UserId { get; set; }
        public int Opened { get; set; }
        public SendNotificationCommand(string message, string userId,int opened)
        {
            Message = message;
            UserId = userId;
            Opened = opened;
        }
    }
    }
