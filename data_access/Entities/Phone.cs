using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Phone
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Model { get; set; }
        
        public int ColorId { get; set; }
        public Color? Color { get; set; }

        [Range(0, 100_000)]
        public decimal Price { get; set; }

        [Range(0, 100_000)]
        public int Memory { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        [Url]
        public string ImagePath { get; set; }
    }
}
