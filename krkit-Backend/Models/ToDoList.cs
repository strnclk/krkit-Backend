namespace krkit_Backend.Models
{
    public class ToDoList
    {
        public int Id { get; set; } // Görev ID
        public string Title { get; set; } // Görev Başlığı
        public string Description { get; set; } // Görev Açıklaması
        public bool IsCompleted { get; set; } = false; // Görev tamamlandı mı?
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Oluşturulma tarihi
    }
}
