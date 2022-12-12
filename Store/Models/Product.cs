using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Title")]
        public string ProductTitle { get; set; }
        [DisplayName("Description")]
        public string? ProductDescription { get; set; }
        [Required]
        [DisplayName("Price")]
        public int ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        [DisplayName("Number of product")]
        public int ProductCount { get; set; }
    }
}
