﻿namespace krkit_Backend.Data.DTOs.ToDoItemDTOs
{
    public class UpdateToDoItemDto
    {
        public string Title { get; set; }  // Görev Başlığı
        public string Description { get; set; }  // Görev Açıklaması
        public bool IsCompleted { get; set; }  // Görev tamamlandı mı?
    }

}
