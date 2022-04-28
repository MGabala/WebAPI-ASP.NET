//----------------------------------------------------------------------------
//CRUD with Authorization Policy with search & filter & pagination metadata. |
//----------------------------------------------------------------------------
namespace WebAPI_ASP.NET6.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, Models.Product>();
        }
    }
}