using Microsoft.AspNetCore.Mvc;
using WebAPI_ASP.NET6.Models;
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
        public ActionResult<ProductDTO> AddProduct(int id)
        {
            var products = ProductsStore.CurrentProduct.Products.FirstOrDefault(x => x.Id == id);
            if (products == null)
            {
                return NotFound();
            }
         
        }

        //}
        //Aktualizacja Description i Quantity
        //[HttpPatch]
        //public ActionResult<ProductDTO> UpdateProduct()
        //{
        //    return NoContent();
        //}

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
