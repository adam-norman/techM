using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistance.Configuration;
using Persistance.Starter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class AppDbContext : IdentityDbContext<Employee>
    {
        #region Migration CLI
        /// <summary>
        /// https://stackoverflow.com/questions/42791317/both-entity-framework-core-and-entity-framework-6-are-installed
        /// in Persistance project cli
        /// dotnet ef --startup-project..\RequestApp\ migrations add Identity_Migration -c AppDbContext
        /// dotnet ef database update -- --environment Production
        /// EntityFrameworkCore\Add-Migration UpdateAnswerAddColumnskey_Value -Context AppDbContext
        /// EntityFrameworkCore\update-database -Context AppDbContext
        /// EntityFrameworkCore\Add-Migration UserAnswers2 -Context AppDbContext
        /// EntityFrameworkCore\update-database -Context AppDbContext
        /// </summary>
        /// <param name="options"></param>
        #endregion
        #region Constructors
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        #endregion
        #region DbSets
         
        public DbSet<RequestForm> RequestForms { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        #endregion
        #region Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.Entity<AspNetRoles>().ToTable(nameof(AspNetRoles), t => t.ExcludeFromMigrations());

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            new DbInitializer(builder).Seed();
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            var result = await base.SaveChangesAsync();
            return result;
        }

        #endregion
    }
}
