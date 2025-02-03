namespace krkit_Backend.Data.Models
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}