using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ViewModels
{
    public class NotificationViewModel
    {
        public string Message { get; set; }
        public string UserId { get; set; }
        public int Opened { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
