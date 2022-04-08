namespace WebAPI.Controllers
{
    [ApiController, Route("api/products")]
    public class ProductController : ControllerBase
    {
      private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //Pobierz całą listę 
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            return Ok(ProductsStore.CurrentProduct.Products);

        }
        //Pobierz produkt o konkretnym ID
        [HttpGet("{id}")]

        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var productToreturn = ProductsStore.CurrentProduct.Products.FirstOrDefault(x => x.Id == id);
            if (productToreturn == null)
            {
                _logger.LogInformation($"There is no product with ID: {id}");
                return NotFound();
                
            }
            return Ok(productToreturn);
        }
       
        //Dodaj nowy typ produktu i go zwróć
        [HttpPost("{id}")]
        public ActionResult<ProductDTO> CreateNewProductType(int id, TypeOfProductCreation productCreation)
        {
            var products = ProductsStore.CurrentProduct.Products.FirstOrDefault(x => x.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            var max = ProductsStore.CurrentProduct.Products.SelectMany(x => x.TypeOfProduct).Max(x => x.Id);
            var final = new TypeOfProduct()
            {
                Id = ++max,
                Color = productCreation.Color,
                Type = productCreation.Type
            };
            products.TypeOfProduct.Add(final);
            return Ok(
                new
                {
                    Id = id,
                    productCreation = final.Id
                });
        }
        //Aktualizacja typu produktu
        [HttpPut("{id}")]
        public ActionResult UpdateTypeOfProduct(int id, int typeid, TypeOfProductUpdate productUpdate)
        {
            var products = ProductsStore.CurrentProduct.Products.FirstOrDefault(x => x.Id == id);
            if(products == null)
            {
                return NotFound();
            }
            var type = products.TypeOfProduct.FirstOrDefault(x => x.Id == typeid);
            if(type == null)
            {
                return NotFound();
            }
          type.Color = productUpdate.Color;
            type.Type = productUpdate.Type;
            return NoContent();
        }


        //Update wartości
        [HttpPatch("{id}")]
        public ActionResult<ProductDTO> UpdateProduct(int id, int typeid, JsonPatchDocument<TypeOfProductUpdate> patch)
        {
            var products = ProductsStore.CurrentProduct.Products.FirstOrDefault(x => x.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            var productBeforeUpdate = products.TypeOfProduct.FirstOrDefault(x=>x.Id== id);
            if(productBeforeUpdate == null)
            {
                return NotFound();
            }
            var productUpdate = new TypeOfProductUpdate()
            {
                Color = productBeforeUpdate.Color,
                Type = productBeforeUpdate.Type,
            };
            patch.ApplyTo(productUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productBeforeUpdate.Color = productUpdate.Color;
            productBeforeUpdate.Type= productUpdate.Type;
            return NoContent();
        }

        //Usuwa produkt
        [HttpDelete("{id}")]
        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            var product = ProductsStore.CurrentProduct.Products.FirstOrDefault(x=>x.Id==id);
            if(product == null)
            {
                return NotFound();
            }
            ProductsStore.CurrentProduct.Products.Remove(product);
            return NoContent();
        }
    }
    
}
