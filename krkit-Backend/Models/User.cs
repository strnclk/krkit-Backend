namespace krkit_Backend.Models
{
    public class User
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}