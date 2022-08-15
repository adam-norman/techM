using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RequestType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Type { get; set; }
        public int IsActive { get; set; }
    }
}