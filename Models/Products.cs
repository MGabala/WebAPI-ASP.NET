namespace WebAPI_ASP.NET6.Models;
public class ProductDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Desc { get; set; }
        public int Quantity { get; set; }
       
      
    }
