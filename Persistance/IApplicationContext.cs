using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<RequestType> RequestTypes { get; set; }
        DbSet<RequestForm> RequestForms { get; set; }
        Task<int> SaveChanges();
    }
}
