using AutoMapper;
using Domain.Models;
using Dto;
using Dto.ViewModels;

namespace Application.Mappers
{
    public class DbModelsProfiles : Profile
    {
        public DbModelsProfiles()
        {
            CreateMap<RequestFormViewModel, RequestForm>()
                .ForSourceMember(src => src.EmployeeName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.RequestType, opt => opt.DoNotValidate()).ReverseMap();
            CreateMap<UserForRegistrationDto, Employee>().ReverseMap();
            CreateMap<NotificationViewModel, Notification>().ReverseMap();
            
        }
    }
}
