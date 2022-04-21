namespace WebAPI.Controllers;
[ApiController, Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepo _productRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductController> _logger;
    private readonly IMailService _mailService;

    //-------------------------------------------------------------------------------------//
    public ProductController(IProductRepo productRepo, IMapper mapper,
        ILogger<ProductController> logger, IMailService mailService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    //-------------------------------------------------------------------------------------//

    //Pobierz całą listę 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var productEntities = await _productRepo.GetAllProductsAsync();
        return Ok(_mapper.Map<IEnumerable<Product>>(productEntities));

    }

    //-------------------------------------------------------------------------------------//

    //Pobierz produkt o konkretnym ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            var product = await _productRepo.GetProductAsync(id);
            if (!await _productRepo.ProductExistAsync(id))
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Product>(product));
        }
        catch (Exception ex)
        {
            _logger?.LogCritical("This is critical error");
            return BadRequest(ex.Message);
        }
    }

    //-------------------------------------------------------------------------------------//
    //Dodaj nowy produkt
    [HttpPost]
    public async Task<ActionResult<Product>> CreateNewProduct(Product id)
    {
        await _productRepo.CreateProduct(id);
        await _productRepo.SaveChangesAsync();
        return Ok();
    }

    //-------------------------------------------------------------------------------------//

    //Usuwa produkt
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(Product id)
    {
        await _productRepo.DeleteProductAsync(id);
        return Ok();
    }
}









//    //Aktualizacja typu produktu
//    [HttpPut("{id}")]
//    public ActionResult UpdateTypeOfProduct(int id, int typeid, TypeOfProductUpdate productUpdate)
//    {
//        var products = _products.Products.FirstOrDefault(x => x.Id == id);
//        if(products == null)
//        {
//        _logger.LogInformation($"There is no product with ID: {id}");
//        return NotFound();
//        }
//        var type = products.TypeOfProduct.FirstOrDefault(x => x.Id == typeid);
//        if(type == null)
//        {
//        _logger.LogInformation($"There is no product with ID: {type}");
//        return NotFound();
//        }
//      type.Color = productUpdate.Color;
//        type.Type = productUpdate.Type;
//        return NoContent();
//    }


//    //Update wartości
//    [HttpPatch("{id}")]
//    public ActionResult<ProductDTO> UpdateProduct(int id, int typeid, JsonPatchDocument<TypeOfProductUpdate> patch)
//    {
//        var products = _products.Products.FirstOrDefault(x => x.Id == id);
//        if (products == null)
//        {
//        _logger.LogInformation($"There is no product with ID: {id}");
//        return NotFound();
//        }
//        var productBeforeUpdate = products.TypeOfProduct.FirstOrDefault(x=>x.Id== id);
//        if(productBeforeUpdate == null)
//        {
//            return NotFound();
//        }
//        var productUpdate = new TypeOfProductUpdate()
//        {
//            Color = productBeforeUpdate.Color,
//            Type = productBeforeUpdate.Type,
//        };
//        patch.ApplyTo(productUpdate, ModelState);
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }
//        productBeforeUpdate.Color = productUpdate.Color;
//        productBeforeUpdate.Type= productUpdate.Type;
//        return NoContent();
//    }


//}
