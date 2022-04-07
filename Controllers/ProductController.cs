namespace WebAPI.Controllers
{
    [ApiController, Route("api/products")]
    public class ProductController : ControllerBase
    {
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


        //Aktualizacja Description i Quantity
        [HttpPatch("id")]
        public ActionResult<ProductDTO> UpdateProduct()
        {
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
