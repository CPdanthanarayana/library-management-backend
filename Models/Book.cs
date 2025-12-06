using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string Author { get; set; } = "";

        [StringLength(1000)]
        public string? Description { get; set; }
    }
}
