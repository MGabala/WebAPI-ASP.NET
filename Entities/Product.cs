//----------------------------------------------------------------------------
//CRUD with Authorization Policy with search & filter & pagination metadata. |
//----------------------------------------------------------------------------
namespace WebAPI_ASP.NET6.Entities;
  public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string? Name { get; set; } = string.Empty; 
        [MaxLength(200)]
        public string? Desc { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
       public bool Test { get; set; }
   
        

        public Product(string name)
        {
        Name = name;
        }
    }