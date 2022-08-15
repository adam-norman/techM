using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ViewModels
{
    public class MessageModel
    {
        public MessageTypeEnum MessageType { get; set; }
        public string Message { get; set; }
        public string InputName { get; set; }
    }
    public enum MessageTypeEnum
    {
        Error = 0,
        Success = 1

    }
}
