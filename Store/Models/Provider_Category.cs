using System.ComponentModel;

namespace Store.Models
{
    public class Provider_Category
    {
        public int ProviderId { get; set; } 
        public int CategoryId { get; set; }
        public Provider  Provider { get; set; }
        public Category Category { get; set; }  
    }
}
