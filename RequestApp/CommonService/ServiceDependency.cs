using Application.Mappers;
using Domain.Models;
using Dto;
using Dto.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance;
using RequestApp.Services;
using RequestApp.Validators;

namespace RequestApp.CommonService
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DbModelsProfiles));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddIdentity<Employee, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Employee>>(TokenOptions.DefaultProvider);
            services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSignalR();
            services.AddTransient<RequestFormService>(); 
                services.AddTransient<NotificationService>();
            #region Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddScoped<IValidator<RequestFormViewModel>, RequestFormValidator>();
            services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            #endregion
            return services;
        }
    }
}
