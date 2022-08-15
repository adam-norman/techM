using AutoMapper;
using Domain.Models;
using Dto.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepositories;
using RequestApp.Filter;

namespace RequestApp.Services
{
    public class RequestFormService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly IRepositoryWrapper _dbContext;


        public RequestFormService(IRepositoryWrapper dbContext, IMapper mapper,UserManager<Employee> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        public List<RequestFormViewModel> GetAllRequests(PaginationFilter filter)
        {  
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var requests = _dbContext.RequestFormRepo.GetAllAsync(el => el.HasApproved == null).Result
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var requestFormViewModels = _mapper.Map<List<RequestFormViewModel>>(requests);
            var requestFormViewModelsTranslated = requestFormViewModels
              .Join(
                _userManager.Users,
                request => request.EmployeeId,
                employee => employee.Id,
              (request, employee) => new { Req = request, Emp = employee })
              .Join(_dbContext.RequestTypeRepo.GetAllAsync().Result.ToList(),
              req => req.Req.RequestTypeId,
              reqType => reqType.Id,
              (req, reqType) => new RequestFormViewModel
              {
                  EmployeeName = req.Emp.FullName,
                  Subject = req.Req.Subject,
                  Body = req.Req.Body,
                  Id = req.Req.Id,
                  EmployeeId = req.Req.EmployeeId,
                  HasApproved = req.Req.HasApproved,
                  RequestDate = req.Req.RequestDate,
                  RequestType = reqType.Type,
                  RequestTypeId = req.Req.RequestTypeId,
              }
              ).ToList();
            return requestFormViewModelsTranslated;
            
        }

        public async Task AddRequestAsync( RequestFormViewModel requestFormViewModel)
        {
            var requestForm = _mapper.Map<RequestForm>(requestFormViewModel);
            await _dbContext.RequestFormRepo.AddAsync(requestForm);
            _dbContext.Save();
        }

        public async Task<List<RequestFormViewModel>> GetMyRequests(string userId, [FromQuery] PaginationFilter filter)
        {
            var requets = await _dbContext.RequestFormRepo.GetAllAsync(r => r.EmployeeId == userId);
            var mapped = _mapper.Map<List<RequestFormViewModel>>(requets);
            return mapped;
        }

        public void UpdateRequest(RequestForm requestForm)
        {
            _dbContext.RequestFormRepo.Update(requestForm);
            _dbContext.Save();
        }

        public async Task<RequestForm> GetRequestById(int Id)
        {
             return await _dbContext.RequestFormRepo.GetAsync(Id);
        }
    }
}
