using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ASP.NET6.Models;
using WebApplication1.Models;

namespace WebAPI_ASP.NET6.Controllers
{
    [Route("api/products/{productId}/typeofproducts")]
    [ApiController]
    public class TypeOfProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<TypeOfProduct>> GetType(int productId)
        {
            var product = ProductsStore.CurrentProduct.Products.FirstOrDefault(c => c.Id == productId);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product.TypeOfProduct);
        }
    
    }
}
