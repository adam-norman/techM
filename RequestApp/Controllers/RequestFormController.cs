using Application.Commands;
using AutoMapper;
using Domain.Models;
using Dto.ViewModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepositories;
using RequestApp.Filter;
using RequestApp.Helpers;
using RequestApp.Services;

namespace RequestApp.Controllers
{
    public class RequestFormController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IMediator _mediator;
        private readonly RequestFormService _requestFormService;
        private readonly IValidator<RequestFormViewModel> _validator;

        public RequestFormController(IRepositoryWrapper dbContext, IMapper mapper,
            IUriService uriService, UserManager<Employee> userManager,
            IMediator mediator
            , RequestFormService requestFormService,
            IValidator<RequestFormViewModel> validator
            )
        {
            _mapper = mapper;
            _uriService = uriService;
            _mediator = mediator;
            _requestFormService = requestFormService;
            _validator = validator;
        }
        [Authorize(Roles = "administrator")]
        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);//MAX PageSize == 50 
            var requests = _requestFormService.GetAllRequests(filter);
            var totalRecords = requests.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<RequestFormViewModel>(requests, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }


        [Authorize(Roles = "employee")]
        [HttpPost("AddRequest")]
        public async Task<IActionResult> AddRequest([FromBody] RequestFormViewModel requestFormViewModel)
        {
            if (Validate(requestFormViewModel, _validator))
            {
                await _requestFormService.AddRequestAsync(requestFormViewModel);
                return StatusCode(201);
            }
            else
            { 
                return BadRequest();
            }
        }
        
        
        [Authorize(Roles = "employee")]
        [HttpGet("GetMyRequests/{userId}")]
        public async Task<IActionResult> GetMyRequests(string userId, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var requests = await _requestFormService.GetMyRequests(userId, filter);
            var totalRecords = requests.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<RequestFormViewModel>(requests, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        
        
        [Authorize(Roles = "administrator")]
        [HttpPost("UpdateRequest")]
        public async Task<IActionResult> UpdateRequest([FromBody] RequestFormViewModel requestFormViewModel)
        {
            if (Validate(requestFormViewModel, _validator))
            {
                var dbRequest =   _requestFormService.GetRequestById(requestFormViewModel.Id).Result;
                if (dbRequest == null)
                {
                    return NotFound("Not Found Request");
                }
                dbRequest.HasApproved = requestFormViewModel.HasApproved;
                _requestFormService.UpdateRequest(dbRequest);
                SendNotification(requestFormViewModel);
            return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }
        private async void SendNotification(RequestFormViewModel request)
        {
            var status = request.HasApproved == 1 ? "Approval" : "Rejected";
            var message = $"{request.RequestType} Request {status}";
            var sendNotificationCommand = new SendNotificationCommand(message, request.EmployeeId, 0);
            await _mediator.Send(sendNotificationCommand);
        }
    }
}
