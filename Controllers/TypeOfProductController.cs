namespace WebAPI.Controllers;
[Route("api/products/{productId}/typeofproducts")]
    [ApiController]
    public class TypeOfProductController : ControllerBase
    {
    private readonly ProductsStore _productsType;

    public TypeOfProductController(ProductsStore productsType)
    {
        _productsType = productsType ?? throw new ArgumentNullException(nameof(productsType));
    }
        [HttpGet]
        public ActionResult<IEnumerable<TypeOfProduct>> GetType(int productId)
        {
            var product = _productsType.Products.FirstOrDefault(c => c.Id == productId);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product.TypeOfProduct);
        }
    }
