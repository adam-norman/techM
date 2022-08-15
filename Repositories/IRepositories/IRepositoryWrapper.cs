using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IRepositoryWrapper:IDisposable
    {
        
        public IRequestFormRepository RequestFormRepo { get; }
        public IRequestTypeRepository RequestTypeRepo { get; }
        public INotificationRepository NotificationRepo { get; }
        
        void Save();
    }
}
