using System.ComponentModel.DataAnnotations;

namespace WebAPI_ASP.NET6.Models
{
    public class ProductsCreation
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Desc { get; set; }
        public int Quantity { get; set; }
    }
}
