using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ViewModels
{
    public class ResultViewModel<T>
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Filter { get; set; }
        public T Data { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Exception { get; set; }
        public ResultViewModel()
        {
            Messages = new HashSet<MessageModel>();
        }
    }
}
