using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace krkit_Backend.Data.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [Column(Order = 0)] // Id her zaman ilk sırada olacak
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
