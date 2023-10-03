namespace Shoping.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        [NotMapped]
        public IFormFile ImageContent { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        //constructor
        public Product()
        {
        }
    }
}