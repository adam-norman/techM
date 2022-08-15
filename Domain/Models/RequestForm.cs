using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RequestForm
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Subject { get; set; }
        [MaxLength(100)]
        public string Body { get; set; }
        public DateTime RequestDate { get; set; }
        public int? HasApproved { get; set; }
        [ForeignKey("RequestTypeId")]
        public int RequestTypeId { get; set; }
        [ForeignKey("EmployeeId")]
        public string EmployeeId  { get; set; }
    }
}
