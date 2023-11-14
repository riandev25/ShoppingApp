using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.EfCore
{
    [Table("product")]
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int Size { get; set; }
        public decimal Price { get; set; }

    }
}
