using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsCount()
        {
            return Ok(ProductsStore.CurrentProduct.Products.Count);
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
        //Dodaj nowy produkt i go zwróć
        [HttpPost("{id}")]
        public ActionResult<ProductDTO> AddProduct()
        {
            var products = ProductsStore.CurrentProduct.Products;
            products.Add(new ProductDTO());
            return Ok();
            //  return NoContent();
        }
        //Aktualizacja Description i Quantity
        [HttpPut]
        public ActionResult<ProductDTO> UpdateProduct()
        {
            return NoContent();
        }
        //Usuwa produkt
        [HttpDelete("{id}")]
        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            var product = ProductsStore.CurrentProduct.Products;
            product.RemoveAt(id);
            return Ok(product);
        }
    }
}
