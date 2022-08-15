using AutoMapper;
using Dto.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepositories;
using RequestApp.Filter;
using RequestApp.Helpers;
using RequestApp.Services;

namespace RequestApp.Controllers
{
    public class NotificationController : ApiBaseController
    {
        private readonly NotificationService _notificationService;
        private readonly IUriService _uriService;
        public NotificationController(IUriService uriService,NotificationService notificationService)
        {
            _notificationService = notificationService;
            _uriService = uriService;
        }

        [Authorize(Roles = "employee")]
        [HttpGet("GetUserNotifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(string userId, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);//MAX PageSize == 50 
            var notifications = _notificationService.GetUserNotifications(userId, validFilter);
            var totalRecords = notifications.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<NotificationViewModel>(notifications, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        [HttpDelete]
        [Authorize(Roles = "employee")]
        [Route("DeleteNotifications/{id}")]
        public async Task<IActionResult> DeleteNotifications(int Id)
        {
            _notificationService.DeleteNotifications(Id);
            return NoContent();
        }
    }
}

