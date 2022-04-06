using System.ComponentModel.DataAnnotations;
using WebAPI_ASP.NET6.Models;

namespace WebApplication1.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Desc { get; set; }
        public int Quantity { get; set; }
        public ICollection<TypeOfProduct> TypeOfProduct { get; set; } = new List<TypeOfProduct>();
    }

    public class ProductsStore
    {
        public List<ProductDTO> Products { get; set; }
        public static ProductsStore CurrentProduct { get; } = new ProductsStore();
        public ProductsStore()
        {
            Products = new List<ProductDTO>()
            {
                new ProductDTO()
                {
                    Id = 1,
                    Name = "First Product",
                    Desc = "This is description",
                    Quantity = 5,
                    TypeOfProduct = new List<TypeOfProduct>()
                    {
                       new TypeOfProduct()
                       {
                           Id=1,
                           Color="Red"
                       }
                    }
                    
                },
                new ProductDTO()
                {
                    Id=2,
                    Name = "Second Product",
                    Desc = String.Empty,
                    Quantity  = 10,
                    TypeOfProduct= new List<TypeOfProduct>()
                    {
                        new TypeOfProduct()
                        {
                            Id=2,
                            Color="Blue"
                        }
                    }
                },
                 new ProductDTO()
                {
                    Id=3,
                    Name = "Third Product",
                    Desc = "",
                    Quantity  = 12,
                    TypeOfProduct = new List<TypeOfProduct>()
                    {
                        new TypeOfProduct()
                        {
                            Id=3,
                            Color="Black"
                        }
                    }
                }
            };
        }

    }

}
