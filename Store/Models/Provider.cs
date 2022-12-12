using Microsoft.Build.Framework;
using System.ComponentModel;

namespace Store.Models
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string ProviderName { get; set; }
        [DisplayName("Description")]
        public string ProviderDescription { get; set; }
        public List<Provider_Category> Provider_Category { get; set; }
        public List<Product>? Products { get; set; }
    }
}
