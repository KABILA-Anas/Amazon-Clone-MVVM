namespace Shoping.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Category(string name)
        {
            Name = name;
        }

        public Category()
        {
        }
    }
}
