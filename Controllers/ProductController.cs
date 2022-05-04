//--------------------------------------------------------------------------------------------------------------------------
//CRUD with Authorization by Policy with search & filter & pagination metadata. Documentation completed for sample methods.|
//--------------------------------------------------------------------------------------------------------------------------
namespace WebAPI.Controllers;
[ApiController, Route("api/products"), ApiVersion("1.0")]
//[Authorize(Policy = "TestPolicy")]

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
  /// <summary>
  /// Metoda pobiera listę produktów, pozwala szukać po określonej nazwie lub frazie. Pozwala ustawić limit danych i przeglądać konkretne strony.
  /// </summary>
  /// <param name="name">Znajdz produkt o określonej nazwie</param>
  /// <param name="searchQuery">Szukaj produkty zawierające dany ciąg</param>
  /// <param name="pageNumber">Określ którą stronę chcesz wylistować</param>
  /// <param name="pageSize">Określa ilość produktów na stronie</param>
  /// <returns></returns>
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

        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

        return Ok(_mapper.Map<IEnumerable<Product>>(productEntities));

    }

    //-------------------------------------------------------------------------------------//
    /// <summary>
    /// Metoda pobiera pojedyńczy produkt o konkretnym ID.
    /// </summary>
    /// <param name="id">Id produktu</param>
    /// <returns></returns>
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
    /// <summary>
    /// Metoda dodaje produkt do bazy danych.
    /// </summary>
    /// <returns></returns>
    //Dodaj nowy produkt
    [HttpPost]
    public async Task<ActionResult<Product>> CreateNewProduct(Product id)
    {
        await _productRepo.CreateProduct(id);
        await _productRepo.SaveChangesAsync();
        return Ok(id);
    }

    //-------------------------------------------------------------------------------------//
    /// <summary>
    /// Metoda pozwala zaktualizować produkt za pomocą JSON patch.
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Usuwa produkt z bazy danych
    /// </summary>
    /// <param name="id"> Id produktu do usunięcia</param>
    /// <returns></returns>
    //Usuwa produkt
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        await _productRepo.DeleteProductAsync(id);
        await _productRepo.SaveChangesAsync();
        return Ok(id);
    }


    //-------------------------------------------------------------------------------------//
    /// <summary>
    /// Metoda pozwala zaktualizować konkretny atrybut produktu.
    /// </summary>
    /// <returns></returns>
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