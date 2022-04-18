﻿

namespace WebAPI.Controllers;
[ApiController, Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMailService _mailService;
    private readonly IProductRepo _productRepo;
    public ProductController(ILogger<ProductController> logger, IMailService mailService, IProductRepo productRepo)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
    }

    //Pobierz całą listę 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductWithoutType>>> GetProducts()
    {
        var productEntities = await _productRepo.GetAllProductsAsync();
        var results = new List<ProductWithoutType>();
        foreach (var productEntity in productEntities)
        {
            results.Add(new ProductWithoutType
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Desc = productEntity.Desc,
                Quantity = productEntity.Quantity,

            });
        }
        return Ok(results);
        //try
        //{
        //   // return Ok(_products.Products);
        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}


    }
}
        //Pobierz produkt o konkretnym ID
    //    [HttpGet("{id}")]

    //    public ActionResult<ProductDTO> GetProduct(int id)
    //    {
    //    try
    //    {
    //        //throw new Exception("Exception sample");
           
    //        var productToreturn = _products.Products.FirstOrDefault(x => x.Id == id);
    //        if (productToreturn == null)
    //        {
    //            _logger.LogInformation($"There is no product with ID: {id}");
    //            return NotFound();

    //        }
    //        return Ok(productToreturn);
    //    } 
    //    catch (Exception ex)
    //    {
    //        _logger.LogCritical($"Its critical log", ex);
    //        return StatusCode(500, "Problem with request");
    //    }
    //    }
       
    //    //Dodaj nowy typ produktu i go zwróć
    //    [HttpPost("{id}")]
    //    public ActionResult<ProductDTO> CreateNewProductType(int id, TypeOfProductCreation productCreation)
    //    {
    //        var products = _products.Products.FirstOrDefault(x => x.Id == id);
    //        if (products == null)
    //        {
    //        _logger.LogInformation($"There is no product with ID: {id}");
    //        return NotFound();
    //        }
    //        var max = _products.Products.SelectMany(x => x.TypeOfProduct).Max(x => x.Id);
    //        var final = new TypeOfProduct()
    //        {
    //            Id = ++max,
    //            Color = productCreation.Color,
    //            Type = productCreation.Type
    //        };
    //        products.TypeOfProduct.Add(final);
    //        return Ok(
    //            new
    //            {
    //                Id = id,
    //                productCreation = final.Id
    //            });
    //    }
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

    //    //Usuwa produkt
    //    [HttpDelete("{id}")]
    //    public ActionResult<ProductDTO> DeleteProduct(int id)
    //    {
    //    try
    //    {
    //        var product = _products.Products.FirstOrDefault(x => x.Id == id);
    //        if (product == null)
    //        {
    //            _logger.LogInformation($"There is no product with ID: {id}");
    //            return NotFound();
    //        }
    //        _products.Products.Remove(product);
    //        _mailService.Send($"Product deleted.",$"Product with ID: {product.Id} was deleted.");
    //        return NoContent();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger?.LogError($"There is an error");
    //        return BadRequest(ex.Message);
    //    }
    //    }
    //}
