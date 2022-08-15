using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ViewModels
{
    public class RequestFormViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime RequestDate { get; set; }
        public int? HasApproved { get; set; }
        public int RequestTypeId { get; set; }
        public string? RequestType { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeId { get; set; }
    }
}
