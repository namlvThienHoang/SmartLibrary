namespace SmartLibrary.Models.EntityModels
{
    public class NotificationUser
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        // Navigation properties
        public virtual Notification Notification { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}