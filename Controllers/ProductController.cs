
namespace WebAPI.Controllers;
[ApiController, Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepo _productRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductController> _logger;
    private readonly IMailService _mailService;
    const int maxSize = 25;

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
    //example of search and filter: ?name=<> / ?searchQuery=<> / ?pageNumber=<> 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
        [FromQuery] string? name, string? searchQuery, int pageNumber = 1, int pageSize = 5 )
    {
        if(pageSize > maxSize)
        {
            pageSize = maxSize;
        }
        var (productEntities, paginationMetadata) = await _productRepo.GetAllProductsAsync(
            name, searchQuery, pageNumber, pageSize);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

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
        return Ok(id);
    }

    //-------------------------------------------------------------------------------------//
    //Aktualizacja typu produktu
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTypeOfProduct(int id, Product product)
    {
        if (!await _productRepo.ProductExistAsync(id))
        {
            return NotFound();
        }
        var producttoUpdate = await _productRepo.GetProductAsync(id);
        if (producttoUpdate == null)
        {
            return NotFound();
        }
        _mapper.Map(producttoUpdate, product);
        await _productRepo.SaveChangesAsync();

        return Ok(product);

    }

    //-------------------------------------------------------------------------------------//

    //Usuwa produkt
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        await _productRepo.DeleteProductAsync(id);
        await _productRepo.SaveChangesAsync();
        return Ok(id);
    }


    //-------------------------------------------------------------------------------------//

    //Update wartości
    [HttpPatch("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, JsonPatchDocument<Product> patch)
    {
        if (!await _productRepo.ProductExistAsync(id))
        {
            return NotFound();
        }
        var productEntity = await _productRepo.GetProductAsync(id);
        if (productEntity == null)
        {
            return NotFound();
        }
        var finalProductUpdate = _mapper.Map<Product>(productEntity);
        patch.ApplyTo(finalProductUpdate, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!TryValidateModel(finalProductUpdate))
        {
            return BadRequest(ModelState);
        }
        _mapper.Map(finalProductUpdate,productEntity);
        await _productRepo.SaveChangesAsync();
        return NoContent();
    }
}