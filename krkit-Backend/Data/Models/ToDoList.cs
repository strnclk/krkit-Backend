namespace krkit_Backend.Data.Models
{
    public class ToDoList : BaseEntity
    {
        public string Title { get; set; } // Görev Başlığı
        public string Description { get; set; } // Görev Açıklaması
        public bool IsCompleted { get; set; } = false; // Görev tamamlandı mı?
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Oluşturulma tarihi
    }
}
