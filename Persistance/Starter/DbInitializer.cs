using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Starter
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;
 

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
             
        }

        public void Seed()
        {
            modelBuilder.Entity<RequestType>().HasData(
                  new RequestType { Id = 1, IsActive = 1, Type = "Promotion" },
                new RequestType { Id = 2, IsActive = 1, Type = "Annual leave" },
                new RequestType { Id = 3, IsActive = 1, Type = "Sick leave" },
                new RequestType { Id = 4, IsActive = 1, Type = "Resign" }
            );
        }
    }
}
