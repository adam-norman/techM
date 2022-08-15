namespace Domain.Models
{
    public class Notification 
    {        
        public int Id { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public int Opened { get; set; } = 0;
        public DateTime? CreatedAt { get; set; }= DateTime.UtcNow;
    }
}
